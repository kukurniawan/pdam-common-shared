using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;

namespace Pdam.Common.Shared.Azure;

/// <summary>
/// azure storage service
/// </summary>
public class StorageService : IFileService
{
    /// <summary>
    /// update blog file
    /// </summary>
    /// <param name="sourceBase64"></param>
    /// <param name="fileName"></param>
    /// <param name="container"></param>
    /// <param name="changeFile"></param>
    /// <param name="access"></param>
    /// <returns></returns>
    public async Task<string> UploadFile(string sourceBase64, string fileName, string container, bool changeFile = true, string access = "Private")
    {
        var filetype = Path.GetExtension(fileName).ToLower();
        var storageConnectionString =  Environment.GetEnvironmentVariable("StorageConnectionString");
        //var cdnStorageUrl = Environment.GetEnvironmentVariable("CdnStorageUrl");
        var imageSource = Convert.FromBase64String(sourceBase64);

        using var stream = new MemoryStream(imageSource);
        var blobContainer = new BlobContainerClient(storageConnectionString, container);
        await blobContainer.CreateIfNotExistsAsync();
        var guidId = changeFile ? Guid.NewGuid() + filetype : fileName;
        var blobClient = blobContainer.GetBlobClient(guidId + filetype);

        await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = GetContentType(filetype) });
        if (access == "Public")
        {
            await blobContainer.SetAccessPolicyAsync(PublicAccessType.Blob);
        }
        return blobClient.Uri.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="imageSource"></param>
    /// <param name="fileName"></param>
    /// <param name="container"></param>
    /// <param name="changeFile"></param>
    /// <param name="access"></param>
    /// <returns></returns>
    public async Task<string> UploadFile(byte[] imageSource, string fileName, string container, bool changeFile = true,  string access = "Private")
    {
        var filetype = Path.GetExtension(fileName).ToLower();
        var storageConnectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
        //var cdnStorageUrl = Environment.GetEnvironmentVariable("CdnStorageUrl");

        using var stream = new MemoryStream(imageSource);
        var blobContainer = new BlobContainerClient(storageConnectionString, container);
        await blobContainer.CreateIfNotExistsAsync();
        var guidId = changeFile ? Guid.NewGuid() + filetype : fileName;
        var blobClient = blobContainer.GetBlobClient(guidId);
        
        await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = GetContentType(filetype) });
        if (access == "Public")
        {
            await blobContainer.SetAccessPolicyAsync(PublicAccessType.Blob);
        }
        return blobClient.Uri.ToString();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="container"></param>
    /// <param name="blobName"></param>
    /// <returns></returns>
    public static string GetAccessToken(string container, string? blobName)
    {
        if (string.IsNullOrEmpty(blobName)) return string.Empty;
        var storageConnectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
        var blobServiceClient = new BlobServiceClient(storageConnectionString);

        //  Gets a reference to the container.
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(container);

        //  Gets a reference to the blob in the container
        var blobClient = blobContainerClient.GetBlobClient(blobName);

        //  Defines the resource being accessed and for how long the access is allowed.
        var blobSasBuilder = new BlobSasBuilder
        {
            StartsOn = DateTime.UtcNow, 
            ExpiresOn = DateTime.UtcNow.AddMinutes(10),
            BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
            Resource = "b",
            Protocol = SasProtocol.Https
        };
        //  Defines the type of permission.
        blobSasBuilder.SetPermissions(BlobSasPermissions.Read);
       
        var keys = GetSharedKey();
        //  Builds an instance of StorageSharedKeyCredential      
        var storageSharedKeyCredential = new StorageSharedKeyCredential(keys.Item1, keys.Item2 + "==");

        //  Builds the Sas URI.
        var sasQueryParameters = blobSasBuilder.ToSasQueryParameters(storageSharedKeyCredential);
        return blobName + "?" + sasQueryParameters;
    }

    /// <summary>
    /// split storage connection string into account name and account key
    /// </summary>
    /// <returns>
    /// 1. Account name
    /// 2. Account key
    /// </returns>
    /// <exception cref="InvalidOperationException"></exception>
    private static (string, string) GetSharedKey()
    {
        var storageConnectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
        if (storageConnectionString == null) throw new InvalidOperationException("Storage Connection String is null");
        
        var strings = storageConnectionString.Split(';');
        var accountName = string.Empty;
        var accountKey = string.Empty;

        foreach (var s in strings)
        {
            var split = s.Split('=');
            switch (split[0])
            {
                case "AccountName":
                    accountName = split[1];
                    break;
                case "AccountKey":
                    accountKey = split[1];
                    break;
            }
        }
        return (accountName, accountKey);
    }
    private static string GetContentType(string filetype)
    {
        return filetype switch
        {
            ".pdf" => "application/pdf",
            ".doc" or ".docx" => "application/msword",
            ".xls" or ".xlsx" => "application/vnd.ms-excel",
            ".jpg" or ".gif" or ".png" or ".bmp" or ".jpeg" => "image/" + filetype.Replace(".", ""),
            ".mp4" => "video/mp4",
            ".svg" => "image/svg+xml",
            ".webp" => "image/webp",
            _ => "text/plain"
        };
    }
}