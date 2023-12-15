using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Entities
{
    public class UniversityFee
    {
        [Key]
        public int UFId { get; set; }
        public string AdmissionFee { get; set; }
        public string PerCreditHourFee { get; set; }
        public University University { get; set; }
        [ForeignKey(nameof(University))]
        public int UId { get; set; }
    }
}
