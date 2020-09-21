using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPAQuizAcademyBasic.Models
{
    public class Quiz
    {
        public int QNo { get; set; }
        public string Question { get; set; }
        public Object Selection { get; set; }
        public string Answer { get; set; }
        public Boolean Hidden { get; set; }
        public Boolean MultiCardGrid { get; set; }
    }
}
