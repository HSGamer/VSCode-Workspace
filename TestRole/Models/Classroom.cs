using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRole.Models
{
    public class Classroom
    {
        public int ClassId { get; set; }
        public int TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}