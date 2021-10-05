using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestRole.Context;
using TestRole.Models;
using TestRole.Models.DTO;

namespace TestRole.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly RoleDbContext dbContext;

        public UserController(RoleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public void Register([FromBody] RegisterDTO value)
        {
            Account account = new Account() {
                UserName = value.UserName,
                Password = value.Password
            };

            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();
            if (value.Role is "Student") {
                dbContext.Students.Add(new Student() {StudentId = account.AccountId});
                dbContext.SaveChanges();
            } else if (value.Role is "Trainer") {
                dbContext.Trainers.Add(new Trainer() {TrainerId = account.AccountId});
                dbContext.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Account account = dbContext.Accounts.Find(id);
            if (account is null) {
                return NotFound();
            }
            dbContext.Accounts.Remove(account);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}