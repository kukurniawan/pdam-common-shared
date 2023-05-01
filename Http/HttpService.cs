using System.Text;
using Newtonsoft.Json;
using Pdam.Common.Shared.Fault;
using Pdam.Common.Shared.Security;

namespace Pdam.Common.Shared.Http;

/// <summary>
/// 
/// </summary>
public class HttpService : IHttpService
{
    private readonly HttpClient _client;

    /// <summary>
    /// 
    /// </summary>
    public HttpService()
    {
        _client = new HttpClient();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="resource"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual async Task<HttpServiceResult<T>> PostAsJson<T, T2>(Uri uri, T2 resource, Action<HttpRequestMessage>? action = null) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        action?.Invoke(request);

        request.Content = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
        var response = await _client.SendAsync(request);
            
        var result = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return HttpServiceResult<T>.Ok(JsonConvert.DeserializeObject<T>(result) ?? throw new InvalidOperationException(), (int)response.StatusCode);
        }

        var failedJson = JsonConvert.DeserializeObject<ErrorDetail>(result);

        return failedJson != null
            ? HttpServiceResult<T>.Fail(failedJson.Description, failedJson.ErrorCode, (int)response.StatusCode)
            : HttpServiceResult<T>.Fail($"Error occurred while performing post to {uri}: {response} - {result}", null, (int)response.StatusCode);
    }

    public async Task<HttpServiceResult<LoginResponse>> DoLogin(LoginRequest loginRequest, Action<HttpRequestMessage>? action = null)
    {
        var uri = new Uri(Environment.GetEnvironmentVariable("AUTH_URL") + "account/login");
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        action?.Invoke(request);

        request.Content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
        var response = await _client.SendAsync(request);
        
        var result = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return HttpServiceResult<LoginResponse>.Ok(JsonConvert.DeserializeObject<LoginResponse>(result) ?? throw new InvalidOperationException(), (int)response.StatusCode);
        }
        
        var failedJson = JsonConvert.DeserializeObject<ErrorDetail>(result);
        return failedJson != null
            ? HttpServiceResult<LoginResponse>.Fail(failedJson.Description, failedJson.ErrorCode, (int)response.StatusCode)
            : HttpServiceResult<LoginResponse>.Fail($"Error occurred while performing post to {uri}: {response} - {result}", null, (int)response.StatusCode);
    }
    
    public async Task<HttpServiceResult<LoginResponse>> RefreshToken(RefreshTokenRequest refreshTokenRequest, Action<HttpRequestMessage>? action = null)
    {
        var uri = new Uri(Environment.GetEnvironmentVariable("AUTH_URL") + "account/refresh-token");
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        action?.Invoke(request);

        request.Content = new StringContent(JsonConvert.SerializeObject(refreshTokenRequest), Encoding.UTF8, "application/json");
        var response = await _client.SendAsync(request);
        
        var result = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return HttpServiceResult<LoginResponse>.Ok(JsonConvert.DeserializeObject<LoginResponse>(result) ?? throw new InvalidOperationException(), (int)response.StatusCode);
        }
        
        var failedJson = JsonConvert.DeserializeObject<ErrorDetail>(result);
        return failedJson != null
            ? HttpServiceResult<LoginResponse>.Fail(failedJson.Description, failedJson.ErrorCode, (int)response.StatusCode)
            : HttpServiceResult<LoginResponse>.Fail($"Error occurred while performing post to {uri}: {response} - {result}", null, (int)response.StatusCode);
    }
}