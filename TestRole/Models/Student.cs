using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRole.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public int? ClassId { get; set; }

        public virtual Classroom Classroom { get; set; }
        public virtual Account Account { get; set; }
    }
}