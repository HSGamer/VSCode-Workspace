using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTracker.Models.DTO;

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

        public AuditLogDTO ToDTO() {
            AuditLogDTO dto = new();
            dto.Id = Id;
            dto.TableName = TableName;
            dto.DateTime = DateTime;
            dto.State = Enum.GetName(State);
            dto.KeyValues = KeyValues is null ? new Dictionary<string, object>() : JsonSerializer.Deserialize<Dictionary<string, object>>(KeyValues);
            dto.OldValues = OldValues is null ? new Dictionary<string, object>() : JsonSerializer.Deserialize<Dictionary<string, object>>(OldValues);
            dto.NewValues = NewValues is null ? new Dictionary<string, object>() : JsonSerializer.Deserialize<Dictionary<string, object>>(NewValues);
            return dto;
        }
    }
}