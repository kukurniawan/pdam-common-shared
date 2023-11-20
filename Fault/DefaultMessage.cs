// ReSharper disable CheckNamespace

namespace Pdam.Common.Shared.Fault
    // ReSharper restore CheckNamespace
{
    /// <summary>
    /// 
    /// </summary>
    public static class DefaultMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public const string ErrorMessage = "Terjadi kesalahan pada server kami, silahkan coba lagi atau hubungi tim support WMI";
        /// <summary>
        /// 
        /// </summary>
        public const string EnvironmentVariableErrorMessage = "Invalid environment variable";
        /// <summary>
        /// 
        /// </summary>
        public const string ConnectionStringFault = "Invalid connection string config";
        /// <summary>
        /// 
        /// </summary>
        public const string NotFound = "Data tidak ditemukan";
        /// <summary>
        /// 
        /// </summary>
        public const string UnprocessableEntity = "Tidak dapat diproses, data sudah terdaftar";
        /// <summary>
        /// 
        /// </summary>
        public const string InActiveAccount = "Tidak dapat melakukan authorisasi, Akun anda tidak aktif";
        /// <summary>
        /// 
        /// </summary>
        public const string LockedAccount = "Tidak dapat melakukan authorisasi, Akun anda terkunci";
        /// <summary>
        /// 
        /// </summary>
        public const string UnauthorizedAccount = "Tidak dapat melakukan authorisasi, kesalahan username atau password";
        /// <summary>
        /// 
        /// </summary>
        public const string UnauthorizedCompany = "Tidak dapat melakukan authorisasi, kesalahan company id";
        /// <summary>
        /// 
        /// </summary>
        public const string InvalidClaim = "Security claim tidak valid atau anda tidak memiliki hak akses";
        /// <summary>
        /// 
        /// </summary>
        public const string InvalidActionArgument = "Argument action tidak valid, data tidak dapat diproses";
        /// <summary>
        /// 
        /// </summary>
        public const string InvalidRequest = "Request tidak valid, data tidak dapat diproses";
        /// <summary>
        /// 
        /// </summary>
        public const string NoClaim = "Tidak dapat melakukan authorisasi, claim authorisasi tidak ditemukan";
        /// <summary>
        /// 
        /// </summary>
        public const string UnauthorizedEmail = "Tidak dapat melakukan authorisasi, email anda tidak memiliki hak akses";
        
        /// <summary>
        /// 
        /// </summary>
        public const string DEFAULT_ERROR_ON_INSERT_OR_UPDATE = "Gagal input baru atau update data, harap hubungi sistem administrator anda.";
    }

    /// <summary>
    /// 
    /// </summary>
    public static class DefaultEventId
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DbUpdateConcurrencyException = "14002";
    }
    
}