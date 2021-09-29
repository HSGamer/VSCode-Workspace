using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using TestTracker.Context;
using TestTracker.Models.DTO;

namespace TestTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditController : ControllerBase
    {
        private readonly PeopleDbContext dbContext;

        public AuditController(PeopleDbContext dbContext) {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<AuditLogDTO> GetAsync()
        {
            return dbContext.Audits.AsEnumerable().Select(log => log.ToDTO());
        }
    }
}