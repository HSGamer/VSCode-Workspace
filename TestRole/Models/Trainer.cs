using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRole.Models
{
    public class Trainer
    {
        public int TrainerId { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Classroom> Classrooms { get; set; }
    }
}