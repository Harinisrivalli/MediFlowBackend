using MediFlow.Database;
using MediFlow.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediFlow.Models
{
    public class Appointment
    {
        public int? Id { get; set; }
        public int? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor doctor { get; set; }
        public int? PatientId { get; set; }
        [ForeignKey("PatientId")]
        public PatientData patient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Notes { get; set; }
        public string Reason { get; set; }
        public string ConsultationType { get; set; }
        public string SelectedSlots { get; set; }
        public string Status { get; set; }
    }
}
