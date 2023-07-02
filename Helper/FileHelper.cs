namespace Pdam.Common.Shared.Helper;

/// <summary>
/// 
/// </summary>
public static class FileHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="fileType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string ToBase64HtmlFormat(byte[] source, string fileType)
    {
        switch (fileType)
        {
            case ".jpg":
                return $"data:image/jpg;base64,{Convert.ToBase64String(source)}";
            case ".png":
                return $"data:image/png;base64,{Convert.ToBase64String(source)}";
            case ".jpeg":
                return $"data:image/jpeg;base64,{Convert.ToBase64String(source)}";
            case ".gif":
                return $"data:image/gif;base64,{Convert.ToBase64String(source)}";
            case ".bmp":
                return $"data:image/bmp;base64,{Convert.ToBase64String(source)}";
            case ".svg":
                return $"data:image/svg+xml;base64,{Convert.ToBase64String(source)}";
            case ".pdf":
                return $"data:application/pdf;base64,{Convert.ToBase64String(source)}";
            case ".docx":
                return $"data:data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,{Convert.ToBase64String(source)}";
            case ".xlsx":
                return $"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,{Convert.ToBase64String(source)}";
            case ".txt":
                return $"data:text/txt;base64,{Convert.ToBase64String(source)}";
            case ".zip":
                return $"data:application/x-zip-compressed;base64,{Convert.ToBase64String(source)}";
            case ".webp":
                return $"data:image/bmp;base64,{Convert.ToBase64String(source)}";
            default:
                throw new ArgumentOutOfRangeException(nameof(fileType), fileType, null);
        }
    }
}