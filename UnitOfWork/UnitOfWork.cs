using Core.Data.DataContext;
using Repository.IRepository;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;



        public IRepositoryBase<object> RepositoryBase { get; }

        public IUniversityCalendarRepository UniversityCalendarRepository { get; }
        public IUniversityDepartmentRepository UniversityDepartmentRepository { get; }
        public IUniversityFeeRepository UniversityFeeRepository { get; }
        public IUniversityDocumentRepository UniversityDocumentRepository { get; }
        public IQuizQuestionRepository QuestionsRepository { get; }
        public IUserRepository UserRepository { get; }
        public UnitOfWork(ApplicationDbContext _db)
        {
            this.db = _db;
            this.UniversityCalendarRepository = new UniversityCalendarRepository(db);
            this.UniversityDepartmentRepository = new UniversityDepartmentRepository(db);
            this.UniversityDocumentRepository = new UniversityDocumentRepository(db);
            this.UniversityFeeRepository = new UniversityFeeRepository(db);
            this.QuestionsRepository = new QuizQuestionRepository(db);
            this.UserRepository = new UserRepository(db);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
