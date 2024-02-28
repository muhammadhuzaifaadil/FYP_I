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

namespace Service.Service
{
    public class FilterService : IFilterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public FilterService(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context=context;
        }
      public List<UniversityDepartments> GetDepartmentsFilteredByName(string keyword)
      {
    var filteredDepartments = _context.UniversityDepartment
        .Where(dept => EF.Functions.Like(dept.DepartmentName, "%" + keyword + "%"))
        .ToList();

    return filteredDepartments;
            }


    }
}
