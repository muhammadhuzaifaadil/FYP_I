using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Entities
{
    public class ConsultationQuestionModel
    {
        [Key]
        public int QuestionID { get; set; }
        [Required]
        public string Questions { get; set; } = "Default";
        [Required]
        public string QuestionsCategory { get; set; } = "Default";
        [Required]
        public string OptionOne { get; set; } = "Default";
        [Required]
        public string OptionTwo { get; set; } = "Default";
        public string OptionThree { get; set; } = "Default";

        public string OptionFour { get; set; } = "Default";
    }
}
