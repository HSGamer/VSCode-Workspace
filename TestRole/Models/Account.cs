using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRole.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }

        public virtual Student Student { get; set; }
        public virtual Trainer Trainer { get; set; }
    }
}