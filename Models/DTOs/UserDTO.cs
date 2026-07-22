using GPACARICOM.Models.Interface;

namespace GPACARICOMAPI.Models.DTO
{
    public class AppUserDTO 
    {
      
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string Telephone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Organization { get; set; } = string.Empty;
    
    }
}

