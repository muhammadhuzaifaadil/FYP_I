using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace FYP__.Controllers
{
    [Route("api/KIET")]
    [ApiController]
    public class KIETUniController : ControllerBase
    {
        private readonly IKIETService _service;
        public KIETUniController(IKIETService service)
        {
            _service = service;
        }
        [HttpGet("KIETCalendar")]
        public IActionResult GetCalendar()
        {
            return Ok(_service.GetUniversityCalendars());
        }

        [HttpGet("KIETDepartments")]
        public IActionResult GetDepartments()
        {
            return Ok(_service.GetUniversityDepartments());
        }

        [HttpGet("KIETFees")]
        public IActionResult GetUniversityFees()
        {
            return Ok(_service.GetUniversityFee());
        }

        [HttpGet("KIETDocuments")]
        public IActionResult GetUniversityDocuments()
        {
            return Ok(_service.GetUniversityDocuments());
        }
    }
}
