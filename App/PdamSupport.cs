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
    
    public static class DefaultError
    {
        public const string By = "System";
        public const string AppTypeName = "Sambungan Baru";
    }

    public static ErrorDetail TrackingNotFound => new("Proses tracking perusahaan anda tidak terdaftar", "1704", HttpStatusCode.UnprocessableEntity);
    public static ErrorDetail DefaultTrackingNotFound => new("Kesalahan default tracker", "1705", HttpStatusCode.UnprocessableEntity);
}

public enum RequestStatus
{
    Active = 1,
    Postpone = 2,
    Reject = 9
}