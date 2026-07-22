using GPACARICOM.Models.Interface;

namespace GPACARICOM.Models
{
    public class UserSignUpRequest : IAuditable
    {
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Organization { get; set; }
        string Telephone { get; set; }
        string email { get; set; }
        string password { get; set; }
        string confirm { get; set; }


        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = "Admin";
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }

    }
}
