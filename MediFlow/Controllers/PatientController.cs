using Microsoft.AspNetCore.Mvc;
using MediFlow.DTO;
using MediFlow.Service;
namespace MediFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;
        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            List<PatientDTOResponse> patients = await _patientService.GetPatients();
            if(patients == null || patients.Count == 0)
            {
                return NotFound(new { status = StatusCode(404), message = "No records" });
            }
            return Ok(new { status = StatusCode(200), message = "Success", data = patients });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromForm] CreatePatientDTO createPatienDTO)
        {
            PatientDTOResponse patients = await _patientService.CreatePatient(createPatienDTO);
            if (patients == null)
            {
                return NotFound(new { status = StatusCode(400), message = "Bad Request" });
            }
            return Ok(new { status = StatusCode(200), message = "Success", data = patients });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, UpdatePatientDTO updatePatientDTO)
        {
            bool status = await _patientService.UpdatePatient(id, updatePatientDTO);
            if (status == false)
            {
                return NotFound(new { status = StatusCode(400), message = "Bad Request" });
            }
            return Ok(new { status = StatusCode(200), message = "Success"});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var status = await _patientService.DeletePatient(id);
            if(status == false)
            {
                return NotFound();
            }
            return Ok(new {status = StatusCode(201), message = "Deleted Successfully"});
        }
    }
}
