using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace FYP__.Controllers
{
    [Route("api/LUMS")]
    [ApiController]
    public class LUMSUniController : ControllerBase
    {
        private readonly ILUMSService _service;
        public LUMSUniController(ILUMSService service)
        {
            _service = service;
        }
        [HttpGet("LUMSCalendar")]
        public IActionResult GetCalendar()
        {
            return Ok(_service.GetUniversityCalendars());
        }

        [HttpGet("LUMSDepartments")]
        public IActionResult GetDepartments()
        {
            return Ok(_service.GetUniversityDepartments());
        }

        [HttpGet("LUMSFees")]
        public IActionResult GetUniversityFees()
        {
            return Ok(_service.GetUniversityFee());
        }

        [HttpGet("LUMSDocuments")]
        public IActionResult GetUniversityDocuments()
        {
            return Ok(_service.GetUniversityDocuments());
        }
    }
}
