using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace FYP__.Controllers
{
    [Route("api/Bahria")]
    [ApiController]
    public class BahriaUniController : ControllerBase
    {
        private readonly IBahriaService _service;
        public BahriaUniController(IBahriaService service) 
        {
            _service = service;
        }
        [HttpGet("BahriaCalendar")]
        public IActionResult GetCalendar()
        {
            return Ok(_service.GetUniversityCalendars());
        }

        [HttpGet("BahriaDepartments")]
        public IActionResult GetDepartments()
        {
            return Ok(_service.GetUniversityDepartments());
        }

        [HttpGet("BahriaFees")]
        public IActionResult GetUniversityFees()
        {
            return Ok(_service.GetUniversityFee());
        }

        [HttpGet("BahriaDocuments")]
        public IActionResult GetUniversityDocuments()
        {
            return Ok(_service.GetUniversityDocuments());
        }
    }
}
