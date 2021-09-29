using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestTracker.Models.DTO
{
    public class AuditLogDTO
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string State { get; set; }
        public Dictionary<string, object> KeyValues { get; set; }
        public Dictionary<string, object> OldValues { get; set; }
        public Dictionary<string, object> NewValues { get; set; }
    }
}