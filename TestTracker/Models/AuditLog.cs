using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestTracker.Models
{
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }

        public virtual EntityState State
        {
            get
            {
                if (OldValues is null)
                {
                    return EntityState.Added;
                }
                else if (NewValues is null)
                {
                    return EntityState.Deleted;
                }
                else
                {
                    return EntityState.Modified;
                }
            }
        }
    }
}