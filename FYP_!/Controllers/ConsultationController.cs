using Core.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Service;

namespace FYP__.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController : Controller
    {

        private readonly ConsultationService _service;
        public ConsultationController(ConsultationService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("GetConsultation")]
        public async Task<ActionResult> GetConsultation(string query)
        {

            try
            {
                string result = await _service.GetConsultation(query);
                if (result == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }



            return View();
        }
        [HttpGet]
        [Route("GetQuestion")]
        public async Task<ActionResult<List<ConsultationQuestionModel>>> GetQuestion()
        {
            return Ok(await _service.GetQuestion());
        }


    }
}
