using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFileSystem.Models
{
    public record FileDTO
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
    }
}