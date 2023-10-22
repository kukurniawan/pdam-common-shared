using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Pdam.Common.Shared.Security
{
    public class RegisterRequest : IRequest<LoginResponse>
    {
        [Required]
        public string UserType { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string MobileNo { get; set; }
        public string PhotoUrl { get; set; }
        public string DeviceId { get; set; }
        public string LoginType { get; set; }
        public string DeviceLanguage { get; set; }
        public string UserRoles { get; set; }
        [Required]
        public string CompanyId { get; set; }
    }
}
