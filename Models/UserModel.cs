using Microsoft.AspNetCore.Identity;

namespace GPACARICOM.Models
{
    public class AppUser
    {
        private int userId { get; set; }
        private string userFirstname { get; set; } = string.Empty;
        private string userLastname { get; set; } = string.Empty;
        private string userEmail { get; set; } = string.Empty;
        private int userRoleId { get; set; }
        private DateTime createdAt { get; set; }
        private DateTime LastLoginAt { get; set; }
 


    }
}
