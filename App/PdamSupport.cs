using System.Net;
using Pdam.Common.Shared.Fault;

namespace Pdam.Common.Shared.App;

public static class PdamSupport
{
    public static class DefaultValue
    {
        public const string By = "System";
        public const string AppTypeName = "Sambungan Baru";
        public const string NewRequestStatusDescription = "New Request";

        public const string NewRequestAcceptedMessage =
            "Pengajuan Pemasangan baru telah diterima dan sedang diproses, silahkan tunggu";
    }
    
    /*
    public static class DefaultError
    {
        public const string By = "System";
        public const string AppTypeName = "Sambungan Baru";
    }*/

    public static ErrorDetail TrackingNotFound => new("Proses tracking perusahaan anda tidak terdaftar", "1701", HttpStatusCode.UnprocessableEntity);
    public static ErrorDetail DefaultTrackingNotFound => new("Kesalahan default tracker", "1702", HttpStatusCode.UnprocessableEntity);
    public static ErrorDetail AppRequestNotFound => new("Kesalahan request sambungan baru", "1703", HttpStatusCode.UnprocessableEntity);
    public static ErrorDetail ActiveTrackingNotFound => new("Tidak ada proses tracking terbaru", "1704", HttpStatusCode.UnprocessableEntity);
    public static ErrorDetail ActiveTrackingRoleNotAllowed => new("Anda tidak memiliki hak akses untuk memperbaharui ticket ini ", "1705", HttpStatusCode.UnprocessableEntity);
}

public enum RequestStatus
{
    Active = 1,
    Postpone = 2,
    Reject = 9
}

public static class RequestType
{
    public const string SAMBUNGAN_BARU = "Sambungan Baru";
    public const string TUTUP_REKENING = "Tutup Rekening";
}