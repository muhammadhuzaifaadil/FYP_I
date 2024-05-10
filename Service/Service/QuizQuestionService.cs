using Core.Data.DataContext;
using Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service.Service
{
    public class QuizQuestionService : IQuizQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public QuizQuestionService(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;

        }
        public async Task<bool> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return false;
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<object>> GetAllQuestions(int count)
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

                return questions;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while fetching questions: {ex.Message}", ex);

            }
        }

        public async Task<Question> GetQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                return null;
            }

            return question;
        }
        public class QuestionQueryDto
        {
            public List<int> CategoryIds { get; set; }
            public int Count { get; set; }
        }
        public async Task<IEnumerable<object>> GetQuestions(QuestionQueryDto query)
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

                return questions;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while fetching questions: {ex.Message}", ex);
            }
        }

        public async Task<bool> PutQuestion(int id, Question question)
        {
            if (id != question.QnId)
            {
                return false;
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
                    return false;
                }
                else
                {
                    throw;

                }
            }

            return true;
        }

        public async Task<IEnumerable<object>> RetrieveAnswers(int[] qnIds)
        {
            var answers = await(_context.Questions
                .Where(x => qnIds.Contains(x.QnId))
                .Select(y => new
                {
                    QnId = y.QnId,
                    QnInWords = y.QnInWords,
                    ImageName = y.ImageName,
                    Options = new string[] { y.Option1, y.Option2, y.Option3, y.Option4 },
                    Answer = y.Answer
                })).ToListAsync();
            return answers;
        }
        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QnId == id);
        }

        Task IQuizQuestionService.PutQuestion(int id, Question question)
        {
            throw new NotImplementedException();
        }
    }
}
