using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prescription.Services;

namespace Prescription.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly PrescriptionService _service;

        public PrescriptionController(PrescriptionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Prescription.Models.Prescription prescription)
        {
            var result = await _service.CreateAsync(prescription);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var list = await _service.GetByPatientIdAsync(patientId);
            return Ok(list);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor(int doctorId)
        {
            var list = await _service.GetByDoctorIdAsync(doctorId);
            return Ok(list);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound(new { message = "Prescription not found." });

            return Ok(new { message = "Prescription deleted successfully." });
        }

    }

}
