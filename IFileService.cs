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
    /// <param name="imageSource"></param>
    /// <param name="fileName"></param>
    /// <param name="container"></param>
    /// <param name="changeFile"></param>
    /// <param name="access"></param>
    /// <returns></returns>
    Task<string> UploadFile(byte[] imageSource, string fileName, string container, bool changeFile = true,
        string access = "Private");
}