// ReSharper disable CheckNamespace

using System.Net;

namespace Pdam.Common.Shared.Fault
    // ReSharper restore CheckNamespace
{
    public static class DefaultMessage
    {
        public const string ErrorMessage = "Something went wrong. Please try again in a few minutes or contact your support team";
        public const string EnvironmentVariableErrorMessage = "Invalid environment variable";
        public const string ConnectionStringFault = "Invalid connection string config";
        public const string NotFound = "Data tidak ditemukan";
        public const string UnprocessableEntity = "Tidak dapat diproses, data sudah terdaftar";
        public const string InActiveAccount = "Tidak dapat melakukan authorisasi, Akun anda tidak aktif";
        public const string LockedAccount = "Tidak dapat melakukan authorisasi, Akun anda terkunci";
        public const string UnauthorizedAccount = "Tidak dapat melakukan authorisasi, kesalahan username atau password";
        public const string UnauthorizedCompany = "Tidak dapat melakukan authorisasi, kesalahan company id";
        public const string InvalidClaim = "Security claim tidak valid atau anda tidak memiliki hak akses";
        public const string InvalidActionArgument = "Argument action tidak valid, data tidak dapat diproses";
        public const string InvalidRequest = "Request tidak valid, data tidak dapat diproses";
        public const string NoClaim = "Tidak dapat melakukan authorisasi, claim authorisasi tidak ditemukan";
        public const string UnauthorizedEmail = "Tidak dapat melakukan authorisasi, email anda tidak memiliki hak akses";
    }

    public static class DefaultEventId
    {
        public const string DbUpdateConcurrencyException = "14002";
    }
    
}