using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Entities
{
    public class UniversityDocuments
    {
        [Key]
        public int UDocId { get; set; }
        public string DocumentRequirement { get; set; }
        public University University { get; set; }
        [ForeignKey(nameof(University))]
        public int UId { get; set; }
    }
}
