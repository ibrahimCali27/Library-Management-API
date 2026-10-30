using Business_.BusinessLayer.BLL;
using Business_.DTOs.Fine_DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FineController(FineService fineService) : ControllerBase
    {
        private readonly FineService _fineService = fineService;

        [Authorize(Roles = "Admin")]
        [HttpPost("create-fine")]
        public async Task<IActionResult> CreateFine()
        {
            var result = await _fineService.CreateFineAsync();

            if (!result)
                return BadRequest();

            return Ok("Fines created successfully.");
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("update-fine")]
        public async Task<IActionResult> UpdateFine()
        {
            await _fineService.UpdateFineAsync();
            return Ok();
        }
        [Authorize]
        [HttpPut("pay-fine")]
        public async Task<IActionResult> PayFine()
        {
            var result = await _fineService.CreateFineAsync();

            if (!result)
                return BadRequest();

            return Ok("Fines created successfully.");
        }
        [Authorize]
        [HttpPost("view-fine")]
        public async Task<IActionResult> ViewFine([FromBody] ViewFineDto dto)
        {
            var fines = await _fineService.ViewFineAsync(dto);

            if (fines.Count == 0)
                return NotFound("No fines found for this user.");

            return Ok(fines);
        }
    }
}
