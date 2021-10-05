using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestRole.Context;
using TestRole.Models;
using TestRole.Models.DTO;
using TestRole.Services;

namespace TestRole.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAccountManager accountManager;

        public UserController(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        [HttpPost]
        public ActionResult Register([FromBody] RegisterDTO value)
        {
            return Ok(new { id = accountManager.addAccount(value) });
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) => accountManager.deleteAccount(id) ? Ok() : NotFound();
    }
}