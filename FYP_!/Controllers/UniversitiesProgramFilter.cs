using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace FYP__.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesProgramFilter : ControllerBase
    {
        private readonly IFilterService _service;
        public UniversitiesProgramFilter(IFilterService service)
        {
            _service = service;
        }
        [HttpGet("UniProgramFilter")]
        public IActionResult GetProgrammes([FromQuery] string keyword)
        {
            return Ok(_service.GetDepartmentsFilteredByName(keyword));
        }
    }
}
