namespace Pdam.Common.Shared;

public interface IFileService
{
    Task<string> UploadFile(string sourceBase64, string fileName, string container, string access = "Private");
}