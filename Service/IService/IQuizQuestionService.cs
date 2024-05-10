using Core.Data.DataContext;
using Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Service.Service.QuizQuestionService;

namespace Service.IService
{
    public interface IQuizQuestionService
    {
        Task<IEnumerable<object>> GetQuestions(QuestionQueryDto query);
        Task<IEnumerable<object>> GetAllQuestions(int count);
        Task<Question> GetQuestion(int id);
        Task PutQuestion(int id, Question question);
        Task<IEnumerable<object>> RetrieveAnswers(int[] qnIds);
        Task<bool> DeleteQuestion(int id);

    }
}
