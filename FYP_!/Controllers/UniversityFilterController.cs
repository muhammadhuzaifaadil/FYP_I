using Core.Data.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FYP__.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityFilterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UniversityFilterController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult FilterUniversities(string? departmentName = null, string? campus = null, string? admissionFees = null, string? perCreditHourFees = null)
        {
            var query1 = _context.UniversityDepartment.AsQueryable();
            var query2 = _context.UniversityFee.AsQueryable();
            
            if (!string.IsNullOrEmpty(departmentName))
            {
                var sanitizedDepartment = SanitizeDepartmentInput(departmentName);
                //query1 = query1.Where(u => u.DepartmentName.ToLower().Replace(" ", "").Contains(sanitizedDepartment));
                query1 = query1.Where(u => u.DepartmentName.ToLower().Contains(sanitizedDepartment));
                //query1 = query1.Where(u => u.DepartmentName == departmentName);
            }

            if (!string.IsNullOrEmpty(campus))
            {
                var sanitizedCampus = SanitizeCampusInput(campus);
                query1 = query1.Where(u => u.Campus.Contains(sanitizedCampus));
            }
            if (!string.IsNullOrEmpty(admissionFees))
            {
                query2 = query2.Where(u => u.AdmissionFee.Contains(admissionFees));
            }

            if (!string.IsNullOrEmpty(perCreditHourFees))
            {
                query2 = query2.Where(u => u.PerCreditHourFee == perCreditHourFees);
            }

            //var universityName = query1.Select(u=>u.University.Name).Distinct().ToList();
            //var universityName2 = query2.Select(u => u.University.Name).Distinct().ToList();
            //var mergedUniversityNames = universityName.Intersect(universityName2).ToList();
            var universityNamesWithIds = query1
               .Select(u => new { Id = u.University.Id, UniversityName = u.University.Name })
               .Distinct()
               .ToList();

            var universityIds = query2
                .Select(u => u.University.Id)
                .Distinct()
                .ToList();

            var mergedUniversityNamesWithIds = universityNamesWithIds
                .Where(u => universityIds.Contains(u.Id))
                .ToList();
            return Ok(mergedUniversityNamesWithIds);
        }
        private string SanitizeCampusInput(string campus)
        {
            // Remove special characters from the campus string
            return new string(campus.Where(char.IsLetterOrDigit).ToArray());
        }
        private string SanitizeDepartmentInput(string department)
        {
            // Remove special characters and convert to lowercase
            //return new string(department.Where(char.IsLetterOrDigit).ToArray()).ToLower();
            return department.ToLower().Trim();
        }

    }
}
