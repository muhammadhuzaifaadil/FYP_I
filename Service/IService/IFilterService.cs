using Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IFilterService
    {
        List<UniversityDepartments> GetDepartmentsFilteredByName(string keyword);
    }
}
