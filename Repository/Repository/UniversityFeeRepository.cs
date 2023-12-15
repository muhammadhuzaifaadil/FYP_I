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
    public class UniversityFeeRepository:RepositoryBase<UniversityFee>,IUniversityFeeRepository
    {
        public UniversityFeeRepository(ApplicationDbContext context):base(context)
        {
            
        }
    }
}
