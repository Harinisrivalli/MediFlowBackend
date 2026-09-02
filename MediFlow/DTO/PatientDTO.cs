using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MediFlow.DTO
{
    public class CreatePatientDTO
    {
        [Required]
        public string fullName { get; set; }
        [Required]
        public int age { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string phoneNo { get; set; }
        [Required]
        public string city { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public string bloodGroup { get; set; }
        public IFormFile profilePhoto { get; set; }
        public bool isActive { get; set; } = true;
        public bool isDeleted { get; set; } = false;
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime? updatedAt { get; set; } = null;
    }

    public class UpdatePatientDTO
    {
        public string fullName { get; set; }
        public int age { get; set; }
        public string email { get; set; }
        public string phoneNo { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        public string gender { get; set; }
        public string bloodGroup { get; set; }
        public string profilePhoto { get; set; }
        public bool isActive { get; set; } = true;
        public bool isDeleted { get; set; } = false;
        public DateTime? updatedAt { get; set; } = DateTime.Now;
    }

    public class PatientDTOResponse
    {
        public int Id { get; set; }
        public string fullName { get; set; }
        public int age { get; set; }
        public string email { get; set; }
        public string phoneNo { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        public string gender { get; set; }
        public string bloodGroup { get; set; }
        public string profilePhoto { get; set; }
        public bool isActive { get; set; } = true;
        public bool isDeleted { get; set; } = false;
        public DateTime createdAt { get; set; }
        public DateTime? updatedAt { get; set; } 
    }
}
