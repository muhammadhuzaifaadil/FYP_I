using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Entities
{
    public class UniversityCalendar
    {
        [Key]
        public int UCId { get; set; }
        public string EventTitle { get; set; }
        public string EventDetails { get; set; }
        public string EventDeadline { get; set; }
        public string EventNotification { get; set; }
        public University University { get; set; }
        [ForeignKey(nameof(University))]
        public int UId { get; set; }
    }
}
