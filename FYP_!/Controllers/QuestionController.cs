using Core.Data.DataContext;
using Core.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FYP__.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class QuestionQueryDto
        {
            public List<int> CategoryIds { get; set; }
            public int Count { get; set; }
        }

        // GET: api/Question
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetQuestions([FromQuery] QuestionQueryDto query)

        {
            try
            {
                var questions = await _context.Questions
    .Where(q => query.CategoryIds.Contains(q.CategoryId))
    .OrderBy(y => Guid.NewGuid())
    .Take(query.Count)
    .Select(x => new
    {
        QnId = x.QnId,
        QnInWords = x.QnInWords,
        ImageName = x.ImageName,
        Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
    })
    .ToListAsync();

                return Ok(questions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // GET: api/Question/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllQuestions(int count)
        {
            try
            {
                var questions = await _context.Questions
                    .OrderBy(y => Guid.NewGuid())
                    .Take(count)
                    .Select(x => new
                    {
                        QnId = x.QnId,
                        QnInWords = x.QnInWords,
                        ImageName = x.ImageName,
                        Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
                    })
                    .ToListAsync();

                return Ok(questions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: api/Question/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // PUT: api/Question/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            if (id != question.QnId)
            {
                return BadRequest();
            }

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Question/GetAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("GetAnswers")]
        public async Task<ActionResult<Question>> RetrieveAnswers(int[] qnIds)
        {
            var answers = await (_context.Questions
                .Where(x => qnIds.Contains(x.QnId))
                .Select(y => new
                {
                    QnId = y.QnId,
                    QnInWords = y.QnInWords,
                    ImageName = y.ImageName,
                    Options = new string[] { y.Option1, y.Option2, y.Option3, y.Option4 },
                    Answer = y.Answer
                })).ToListAsync();
            return Ok(answers);
        }

        // DELETE: api/Question/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QnId == id);
        }
    }
}
