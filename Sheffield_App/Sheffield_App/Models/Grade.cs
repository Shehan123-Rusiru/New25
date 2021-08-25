using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sheffield_App.Models
{
    public class Grade
    {
        public int ID { get; set; }

        
        public string Student_NO { get; set; }

        public string Subject_ID { get; set; }

        public float Marks { get; set; }

        public string Grades { get; set; }
    }
}
