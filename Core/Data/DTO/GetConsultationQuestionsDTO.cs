using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.DTO
{
    public class GetConsultationQuestionsDTO
    {
        public int QuestionID { get; set; }

        public string Questions { get; set; }

        public string QuestionsCategory { get; set; }

        public string OptionOne { get; set; }

        public string OptionTwo { get; set; }
        public string OptionThree { get; set; } = "Null";

        public string OptionFour { get; set; } = "Null";
    }
}
