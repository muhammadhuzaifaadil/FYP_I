using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace FYP__.Controllers
{
    [Route("api/Comsats")]
    [ApiController]
    public class ComsatsUniController : ControllerBase
    {
        private readonly IComsatService _service;
        public ComsatsUniController(IComsatService service)
        {
            _service = service;
        }
        [HttpGet("ComsatsCalendar")]
        public IActionResult GetCalendar()
        {
            return Ok(_service.GetUniversityCalendars());
        }
        [HttpGet("ComsatsDepartments")]
        public IActionResult GetDepartments()
        {
            return Ok(_service.GetUniversityDepartments());
        }

        [HttpGet("ComsatsFees")]
        public IActionResult GetUniversityFees()
        {
            return Ok(_service.GetUniversityFee());
        }

        [HttpGet("ComsatsDocuments")]
        public IActionResult GetUniversityDocuments()
        {
            return Ok(_service.GetUniversityDocuments());
        }
    }
}
