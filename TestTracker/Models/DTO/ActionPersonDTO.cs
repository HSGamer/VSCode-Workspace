using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTracker.Models.DTO
{
    public class ActionPersonDTO
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string address { get; set; }
    }
}