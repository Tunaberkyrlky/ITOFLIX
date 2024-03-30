using System.ComponentModel.DataAnnotations;

namespace ITOFLIX.DTO.Requests
{
    public class UserUpdateRequest
    {
        [StringLength(100, MinimumLength = 2)]
        public string? UserName { get; set; }

        [EmailAddress]
        [StringLength(100, MinimumLength = 5)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(30)]
        public string? PhoneNumber { get; set; }
    }
}
