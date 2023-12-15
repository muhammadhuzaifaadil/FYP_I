using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Entities
{
        public class University
        {
            //name,location,description,contact,email 
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Location { get; set; }
            public string Contact { get; set; }
            public string Email { get; set; }
            public ICollection<UniversityDepartments> UniversityDepartments { get; set; }
            public ICollection<UniversityDocuments> UniversityDocuments { get; set; }
            public ICollection<UniversityFee> UniversityFees { get; set; }
            public ICollection<UniversityCalendar> UniversityCalendars { get; set; }
    }
}
