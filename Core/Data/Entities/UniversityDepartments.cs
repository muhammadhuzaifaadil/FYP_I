using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Entities
{
    public class UniversityDepartments
    {
        [Key]
        public int UDId { get; set; }
        public string DepartmentName { get; set; }
        public string Campus { get; set; }
        public University University { get; set; }
        [ForeignKey(nameof(University))]
        public int UId { get; set;}
    }
}
