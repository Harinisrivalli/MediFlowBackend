using MediFlow.DTO;
using MediFlow.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace MediFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Doctor : ControllerBase
    {
        private readonly DoctorService _service;

        public Doctor(DoctorService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromForm]CreateDoctorDTO doctorDTO)
        {
            doctorDTO.availabilitySlot = JsonSerializer.Deserialize<List<DTO.AvailabilitySlot>>(Request.Form["availabilitySlot"]);
            if (doctorDTO == null)
                return BadRequest();

            var response = await _service.CreateDoctor(doctorDTO);

            if (response == null)
                return UnprocessableEntity();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctors(int id)
        {
            if (id <= 0)
                return BadRequest();

            var response = await _service.GetDoctorById(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var response = await _service.GetDoctor();

            if (response == null)
                return UnprocessableEntity();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctors(int id)
        {
            var status = await _service.DeleteDoctor(id);

            if (status == true)
                return NoContent();

            return UnprocessableEntity();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateDoctors(int id, [FromBody] UpdateDoctorDTO updateDoctorDTO)
        {
            var response = await _service.UpdateDoctor(updateDoctorDTO);

            if (id != updateDoctorDTO.Id)
                return BadRequest();

            if (response == null)
                return UnprocessableEntity();

            return Ok(response);
        }
    }
}