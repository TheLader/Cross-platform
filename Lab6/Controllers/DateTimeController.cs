using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DateTimeController : ControllerBase
    {
        [HttpGet("convert")]
        public IActionResult ConvertDateTime([FromQuery] string inputDateTime)
        {
            try
            {
                // Спроба розпарсити вхідний формат дати
                if (!DateTime.TryParse(inputDateTime, null, DateTimeStyles.AdjustToUniversal, out DateTime dateTime))
                {
                    return BadRequest("Invalid date format.");
                }

                // Конвертація часу в GMT+2/3 (Україна)
                var ukraineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Kiev");
                var ukraineTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime(), ukraineTimeZone);

                return Ok(new
                {
                    OriginalTime = inputDateTime,
                    ConvertedTime = ukraineTime.ToString("yyyy-MM-ddTHH:mm:ssK")
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
