using Pdam.Common.Shared.Azure;
using System.Threading.Tasks;

namespace Pdam.Common.Shared;

/// <summary>
/// 
/// </summary>
public interface IFileService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sourceBase64"></param>
    /// <param name="fileName"></param>
    /// <param name="container"></param>
    /// <param name="changeFile"></param>
    /// <param name="access"></param>
    /// <returns></returns>
    Task<string> UploadFile(string sourceBase64, string fileName, string container, bool changeFile = true,
        string access = "Private");

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sourceBase64"></param>
    /// <param name="fileName"></param>
    /// <param name="fileType"></param>
    /// <param name="container"></param>
    /// <returns></returns>
    Task<string> UploadBackup(string sourceBase64, string fileName, string fileType, string container);

    /// <summary>
    /// list backup data
    /// </summary>
    /// <param name="key"></param>
    /// <param name="container"></param>
    /// <returns></returns>
    Task<List<BlobModel>> ListBackup(string key, string container);

    /// <summary>
    /// list backup data
    /// </summary>
    /// <param name="key"></param>
    /// <param name="container"></param>
    /// <returns></returns>
    Task<BlobModel?> GetBackup(string key, string container);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="imageSource"></param>
    /// <param name="fileName"></param>
    /// <param name="container"></param>
    /// <param name="changeFile"></param>
    /// <param name="access"></param>
    /// <returns></returns>
    Task<string> UploadFile(byte[] imageSource, string fileName, string container, bool changeFile = true,
        string access = "Private");
}