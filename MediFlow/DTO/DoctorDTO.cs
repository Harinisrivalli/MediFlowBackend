using System.ComponentModel.DataAnnotations;

namespace MediFlow.DTO
{
       public class AvailabilitySlot
       {
            public string day { get; set; }
            public string? startTime { get; set; }
            public string? endTime { get; set; }
            public bool isAvailable { get; set; }
       }

       public class CreateDoctorDTO
    {
            public int Id { get; set; }
            public string fullName { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string phoneNo { get; set; }
            public string gender { get; set; }
            public string dob { get; set; }
            public IFormFile profilePhoto { get; set; }
            public string specialization { get; set; }
            public string qualification { get; set; }
            public string licenseNo { get; set; }
            public int experience { get; set; }
            public int consultationFee { get; set; }
            public string about { get; set; }

            public List<AvailabilitySlot> availabilitySlot { get; set; }
            public string status { get; set; }
            public bool isActive { get; set; } = true;
            public bool isDeleted { get; set; } = false;
            public DateTime createdAt { get; set; } = DateTime.Now;
            public DateTime? updatedAt { get; set; } = null;
       }

       public class UpdateDoctorDTO
        {
            public int Id { get; set; }
            public string fullName { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string phoneNo { get; set; }
            public string gender { get; set; }
            public string dob { get; set; }
            public string profilePhoto { get; set; }
            public string specialization { get; set; }
            public string qualification { get; set; }
            public string licenseNo { get; set; }
            public int experience { get; set; }
            public int consultationFee { get; set; }
            public string about { get; set; }

            public List<AvailabilitySlot> availabilitySlot { get; set; }
            public string status { get; set; }
            public bool isActive { get; set; }
            public bool isDeleted { get; set; }
            public DateTime createdAt { get; set; }
            public DateTime? updatedAt { get; set; }
    }

    public class CreateDoctorDTOResp
    {
        public int Id { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNo { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string profilePhoto { get; set; }
        public string specialization { get; set; }
        public string qualification { get; set; }
        public string licenseNo { get; set; }
        public int experience { get; set; }
        public int consultationFee { get; set; }
        public string about { get; set; }

        public List<AvailabilitySlot> availabilitySlot { get; set; }
        public string status { get; set; }

        public bool isActive { get; set; } = true;
        public bool isDeleted { get; set; } = false;
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime? updatedAt { get; set; } = null;
    }
}
