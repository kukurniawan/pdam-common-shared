using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Pdam.Common.Shared.Azure;

public class StorageService : IFileService
{
    
    public async Task<string> UploadFile(string sourceBase64, string fileName, string container,  string access = "Private")
    {
        var filetype = Path.GetExtension(fileName).ToLower();

        var storageConnectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
        //var cdnStorageUrl = Environment.GetEnvironmentVariable("CdnStorageUrl");
        var imageSource = Convert.FromBase64String(sourceBase64);

        using var stream = new MemoryStream(imageSource);
        var blobContainer = new BlobContainerClient(storageConnectionString, container);
        await blobContainer.CreateIfNotExistsAsync();
        var guidId = Guid.NewGuid().ToString();
        var blobClient = blobContainer.GetBlobClient(guidId + filetype);

        await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = GetContentType(filetype) });
        if (access == "Public")
        {
            await blobContainer.SetAccessPolicyAsync(PublicAccessType.Blob);
        }
        return blobClient.Uri.ToString();
    }
    
    private static string GetContentType(string filetype)
    {
        switch (filetype)
        {
            case ".pdf":
                return "application/pdf";
            case ".doc":
            case ".docx":
                return "application/msword";
            case ".xls":
            case ".xlsx":
                return "application/vnd.ms-excel";
            case ".jpg":
            case ".gif":
            case ".png":
            case ".bmp":
            case ".jpeg":
                return "image/" + filetype.Replace(".", "");
            case ".mp4":
                return "video/mp4";
            case ".svg":
                return "image/svg+xml";
            case ".webp":
                return "image/webp";
            default:
                return "text/plain";
        }
    }
}