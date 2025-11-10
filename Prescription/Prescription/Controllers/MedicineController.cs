using Microsoft.AspNetCore.Mvc;
using Prescription.Services;

namespace Prescription.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicineController : ControllerBase
    {
        private readonly MedicineService _service;

        public MedicineController(MedicineService service)
        {
            _service = service;
        }

        [HttpPost("decrement/{medicineId}")]
        public async Task<IActionResult> DecrementRemaining(int medicineId)
        {
            var success = await _service.DecrementRemainingAsync(medicineId);

            if (!success)
                return BadRequest(new { message = "Medicine not found or remaining quantity is already 0" });

            return Ok(new { message = "Remaining quantity decremented successfully" });
        }
    }
}
