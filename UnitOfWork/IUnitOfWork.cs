using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {

        IRepositoryBase<object> RepositoryBase { get; }
        IUniversityCalendarRepository UniversityCalendarRepository { get; }
        IUniversityDepartmentRepository UniversityDepartmentRepository { get; }
        IUniversityDocumentRepository UniversityDocumentRepository { get; }
        IUniversityFeeRepository UniversityFeeRepository { get; }
        IQuizQuestionRepository QuestionsRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
