using MediFlow.Controllers;

namespace MediFlow.DTO
{
    public enum Status
    {
        Booked = 1,
        Cancelled,
        Completed,
    }

    public class AppointmentDTO
    {
        public int? Id { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Notes { get; set; }
        public string Reason { get; set; }
        public string ConsultationType { get; set; }
        public string SelectedSlots { get; set; }
        public Status Status { get; set; }
    }

    public class AppointmentDTOResponse
    {
        public int? Id { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public CreateDoctorDTOResp doctor { get; set; }

        public PatientDTOResponse patient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Notes { get; set; }
        public string Reason { get; set; }
        public string ConsultationType { get; set; }
        public string SelectedSlots { get; set; }
        public Status Status { get; set; }
    }
}
