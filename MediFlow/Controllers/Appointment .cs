using MediFlow.DTO;
using MediFlow.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Appointment : ControllerBase
    {
        private readonly AppointmentService _service;

        public Appointment(AppointmentService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById([FromRoute] int id)
        {
            var resp = await _service.GetAppointmentById(id);
            if (resp == null)
                return NotFound(new { message = "No records" });
            return Ok(new { message = resp });
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointment(
            [FromQuery] int? page,
            [FromQuery] int? pageSize)
        {
            var resp = await _service.GetAppointment();
            if(resp == null || resp.Count == 0)
                return NotFound(new { message = "No records" });
            return Ok(new { message = resp });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(
            [FromBody] AppointmentDTO appointment)
        {
            var resp = await _service.CreateAppointment(appointment);
            if (resp == null)
                return BadRequest(new { message = "Failed to create appointment" });
            return Ok(new { data = resp });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAppointment()
        {
            var Id = User.FindFirst("Id")?.Value;
            var resp = await _service.DeleteAppointment(Convert.ToInt32(Id));

            if (resp)
                return NoContent();

            return NotFound(new {  message = "Appointment not found" });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAppointment(
            [FromBody] AppointmentDTO appointment,
            [FromRoute] int id)
        {
            var resp = await _service.UpdateAppointment(appointment);

            if (resp == null)
                return NotFound();

            return Ok(new { data = resp });
        }

        [HttpGet("doctor/{id}")]
        public async Task<IActionResult> GetDoctorAppointments([FromRoute] int id, [FromQuery] DateTime date)
        {
            var respone = await _service.GetDoctorAppointments(id, date);
            if(respone == null)
                return NotFound(new { message = "No records" });
            return Ok(new { message = respone });
        }

        [HttpGet("patient/{id}")]
        public async Task<IActionResult> GetPatientAppointments(int id)
        {
            var respone = await _service.GetPatientAppointments(id);
            if (respone == null)
                return NotFound(new { message = "No records" });
            return Ok(new { message = respone });
        }
    }
}