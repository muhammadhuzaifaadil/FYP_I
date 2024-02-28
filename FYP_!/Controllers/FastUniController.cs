using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace FYP__.Controllers
{
    [Route("api/Fast")]
    [ApiController]
    public class FastUniController : ControllerBase
    {
        private readonly IFastService _service;
        public FastUniController(IFastService uniservice) 
        {
        
            _service = uniservice;
        }
        [HttpGet("FastCalendar")]
        public IActionResult GetCalendar()
        {
            return Ok(_service.GetUniversityCalendars());
        }

        [HttpGet("FastDepartments")]
        public IActionResult GetDepartments()
        {
            return Ok(_service.GetUniversityDepartments());
        }

        [HttpGet("FastFees")]
        public IActionResult GetUniversityFees()
        {
            return Ok(_service.GetUniversityFee());
        }

        [HttpGet("FastDocuments")]
        public IActionResult GetUniversityDocuments()
        {
            return Ok(_service.GetUniversityDocuments());
        }
    }
}
