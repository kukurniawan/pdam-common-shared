using System.Net;
using Pdam.Common.Shared.Fault;

namespace Pdam.Common.Shared.App;

/// <summary>
/// 
/// </summary>
public static class PdamSupport
{
    /// <summary>
    /// 
    /// </summary>
    public static class DefaultValue
    {
        /// <summary>
        /// 
        /// </summary>
        public const string By = "System";
        /// <summary>
        /// 
        /// </summary>
        public const string AppTypeName = "Sambungan Baru";
        /// <summary>
        /// 
        /// </summary>
        public const string NewRequestStatusDescription = "New Request";

        /// <summary>
        /// 
        /// </summary>
        public const string NewRequestAcceptedMessage =
            "Pengajuan Pemasangan baru telah diterima dan sedang diproses, silahkan tunggu";
    }
    
    /*
    public static class DefaultError
    {
        public const string By = "System";
        public const string AppTypeName = "Sambungan Baru";
    }*/

    /// <summary>
    /// 
    /// </summary>
    public static ErrorDetail TrackingNotFound => new("Proses tracking perusahaan anda tidak terdaftar", "1701", HttpStatusCode.UnprocessableEntity);
    /// <summary>
    /// 
    /// </summary>
    public static ErrorDetail DefaultTrackingNotFound => new("Kesalahan default tracker", "1702", HttpStatusCode.UnprocessableEntity);
    /// <summary>
    /// 
    /// </summary>
    public static ErrorDetail AppRequestNotFound => new("Kesalahan request sambungan baru", "1703", HttpStatusCode.UnprocessableEntity);
    /// <summary>
    /// 
    /// </summary>
    public static ErrorDetail ActiveTrackingNotFound => new("Tidak ada proses tracking terbaru", "1704", HttpStatusCode.UnprocessableEntity);
    /// <summary>
    /// 
    /// </summary>
    public static ErrorDetail ActiveTrackingRoleNotAllowed => new("Anda tidak memiliki hak akses untuk memperbaharui ticket ini ", "1705", HttpStatusCode.UnprocessableEntity);

    /// <summary>
    /// 
    /// </summary>
    public static ErrorDetail NoActiveSpk =>
        new ErrorDetail("SPK tidak ditemukan", "1706", HttpStatusCode.UnprocessableEntity);
}

/// <summary>
/// 
/// </summary>
public enum RequestStatus
{
    /// <summary>
    /// 
    /// </summary>
    Active = 1,
    /// <summary>
    /// 
    /// </summary>
    Postpone = 2,
    /// <summary>
    /// 
    /// </summary>
    Reject = 9
}

/// <summary>
/// 
/// </summary>
public static class RequestType
{
    /// <summary>
    /// 
    /// </summary>
    public const string SAMBUNGAN_BARU = "Sambungan Baru";
    /// <summary>
    /// 
    /// </summary>
    public const string TUTUP_REKENING = "Tutup Rekening";
}

/// <summary>
/// Jenis bangunan
/// </summary>
public enum BuildingType
{
    /// <summary>
    /// rumah kayu
    /// </summary>
    RumahKayu,
    /// <summary>
    /// rumah beton
    /// </summary>
    RumahBeton,
    /// <summary>
    /// toko
    /// </summary>
    Toko,
    /// <summary>
    /// kios
    /// </summary>
    Kios,
    /// <summary>
    /// kantor
    /// </summary>
    Kantor,
    /// <summary>
    /// fasilitas umum
    /// </summary>
    FasilitasUmum,
    /// <summary>
    /// lainnya
    /// </summary>
    Lainnya
}

/// <summary>
/// peruntukan bangunan
/// </summary>
public enum BuildingDesignation
{
    /// <summary>
    /// tempat tinggal
    /// </summary>
    TempatTinggal,
    /// <summary>
    /// tempat usaha
    /// </summary>
    TempatUsaha,
    /// <summary>
    /// sosial
    /// </summary>
    Sosial,
    /// <summary>
    /// lainnya
    /// </summary>
    Lainnya
}