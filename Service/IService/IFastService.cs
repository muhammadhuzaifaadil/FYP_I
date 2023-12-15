using Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IFastService
    {
        List<UniversityCalendar> GetUniversityCalendars();

        List<UniversityDepartments> GetUniversityDepartments();

        List<UniversityDocuments> GetUniversityDocuments();

        List<UniversityFee> GetUniversityFee();
    }
}
