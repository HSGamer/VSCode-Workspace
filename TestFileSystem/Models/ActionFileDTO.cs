using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFileSystem.Models
{
    public record ActionFileDTO
    {
        public string FileName { get; set; }
        public string Url { get; set; }

        public virtual Uri Uri
        {
            get
            {
                return new Uri(Url);
            }

            set
            {
                Url = value.OriginalString;
            }
        }

        public FileEntry ToEntry() {
            return new FileEntry() {
                FileName = this.FileName,
                Url = this.Url
            };
        }
    }
}