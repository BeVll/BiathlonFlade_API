using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MB_API.Data.Entities.Identity
{
    public class UserEntity : IdentityUser<int>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Image { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string National { get; set; }
        public bool IsLightTheme { get; set; }
        public string? Instagram { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Telegram { get; set; }
        public int? CountryId { get; set; }
        public string Status { get; set; }
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public virtual CountryEntity Country {  get; set; } 
    }
}
