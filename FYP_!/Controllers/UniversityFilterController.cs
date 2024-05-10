using Core.Data.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        //[HttpGet]
        //public IActionResult FilterUniversities(string[]? departmentNames = null, string[]? campuses = null, string? admissionFees = null, string? perCreditHourFees = null)
        //{
        //    var query1 = _context.UniversityDepartment.AsQueryable();
        //    var query2 = _context.UniversityFee.AsQueryable();

        //    // Apply initial filters that can be translated to SQL
        //    if (departmentNames != null && departmentNames.Any())
        //    {
        //        var sanitizedDepartments = departmentNames.Select(SanitizeDepartmentInput);
        //        query1 = query1.Where(u => sanitizedDepartments.Contains(u.DepartmentName.ToLower()));
        //    }

        //    if (campuses != null && campuses.Any())
        //    {
        //        var sanitizedCampuses = campuses.Select(SanitizeCampusInput);
        //        query1 = query1.Where(u => sanitizedCampuses.Contains(u.Campus.ToLower()));
        //    }

        //    // Materialize the query and perform the rest of the filtering in memory
        //    var universityDepartments = query1.AsEnumerable();

        //    if (!string.IsNullOrEmpty(admissionFees))
        //    {
        //        query2 = query2.Where(u => u.AdmissionFee.Contains(admissionFees));
        //    }

        //    if (!string.IsNullOrEmpty(perCreditHourFees))
        //    {
        //        query2 = query2.Where(u => u.PerCreditHourFee == perCreditHourFees);
        //    }

        //    var universityIds = query2.Select(u => u.University.Id).Distinct().ToList();

        //    var mergedUniversityNamesWithIds = universityDepartments
        //        .Where(u => universityIds.Contains(u.University.Id))
        //        .Select(u => new { Id = u.University.Id, UniversityName = u.University.Name })
        //        .Distinct()
        //        .ToList();

        //    return Ok(mergedUniversityNamesWithIds);
        //}
        //private string SanitizeCampusInput(string campus)
        //{
        //    // Remove special characters from the campus string
        //    return new string(campus.Where(char.IsLetterOrDigit).ToArray());
        //}

        //private string SanitizeDepartmentInput(string department)
        //{
        //    // Remove special characters and convert to lowercase
        //    return department.ToLower().Trim();
        //}

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
        //[HttpGet]
        //public IActionResult FilterUniversities([FromQuery] string[]? departmentNames = null, [FromQuery] string[]? campuses = null, string? admissionFees = null, string? perCreditHourFees = null)
        //{
        //    var query1 = _context.UniversityDepartment.AsQueryable();
        //    var query2 = _context.UniversityFee.AsQueryable();

        //    if (departmentNames != null && departmentNames.Length > 0)
        //    {
        //        // Filter by multiple departments

        //        query1 = query1.Where(u => departmentNames.Contains(u.DepartmentName.ToLower()));
        //    }

        //    if (campuses != null && campuses.Length > 0)
        //    {
        //        // Filter by multiple campuses
        //        query1 = query1.Where(u => campuses.Contains(u.Campus));
        //    }

        //    if (!string.IsNullOrEmpty(admissionFees))
        //    {
        //        query2 = query2.Where(u => u.AdmissionFee.Contains(admissionFees));
        //    }

        //    if (!string.IsNullOrEmpty(perCreditHourFees))
        //    {
        //        query2 = query2.Where(u => u.PerCreditHourFee == perCreditHourFees);
        //    }

        //    var universityNamesWithIds = query1
        //        .Select(u => new { Id = u.University.Id, UniversityName = u.University.Name })
        //        .Distinct()
        //        .ToList();

        //    var universityIds = query2
        //        .Select(u => u.University.Id)
        //        .Distinct()
        //        .ToList();

        //    var mergedUniversityNamesWithIds = universityNamesWithIds
        //        .Where(u => universityIds.Contains(u.Id))
        //        .ToList();

        //    return Ok(mergedUniversityNamesWithIds);
        //}
        //[HttpGet]
        //public IActionResult FilterUniversities([FromQuery] string[]? departmentNames = null, [FromQuery] string[]? campuses = null, string? admissionFees = null, string? perCreditHourFees = null)
        //{
        //    var query1 = _context.UniversityDepartment.AsQueryable();
        //    var query2 = _context.UniversityFee.AsQueryable();

        //    if (departmentNames != null && departmentNames.Length > 0)
        //    {
        //        // Sanitize department names
        //        var sanitizedDepartments = departmentNames.Select(SanitizeDepartmentInput).ToArray();

        //        // Filter by multiple departments
        //        query1 = query1.Where(u => sanitizedDepartments.Contains(u.DepartmentName.ToLower()));
        //    }

        //    if (campuses != null && campuses.Length > 0)
        //    {
        //        // Filter by multiple campuses
        //        query1 = query1.Where(u => campuses.Contains(u.Campus));
        //    }

        //    if (!string.IsNullOrEmpty(admissionFees))
        //    {
        //        query2 = query2.Where(u => u.AdmissionFee.Contains(admissionFees));
        //    }

        //    if (!string.IsNullOrEmpty(perCreditHourFees))
        //    {
        //        query2 = query2.Where(u => u.PerCreditHourFee == perCreditHourFees);
        //    }

        //    var universityNamesWithIds = query1
        //        .Select(u => new { Id = u.University.Id, UniversityName = u.University.Name })
        //        .Distinct()
        //        .ToList();

        //    var universityIds = query2
        //        .Select(u => u.University.Id)
        //        .Distinct()
        //        .ToList();

        //    var mergedUniversityNamesWithIds = universityNamesWithIds
        //        .Where(u => universityIds.Contains(u.Id))
        //        .ToList();

        //    return Ok(mergedUniversityNamesWithIds);
        //}
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
