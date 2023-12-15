using Core.Data.DataContext;
using Core.Data.Entities;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UniversityDepartmentRepository:RepositoryBase<UniversityDepartments>,IUniversityDepartmentRepository
    {
        public UniversityDepartmentRepository(ApplicationDbContext context):base(context)
        {
            
        }
    }
}
