using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRole.Context;
using TestRole.Models;
using TestRole.Models.DTO;

namespace TestRole.Services
{
    public class AccountManager : IAccountManager
    {
        private readonly RoleDbContext dbContext;

        public AccountManager(RoleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int addAccount(RegisterDTO registerDTO)
        {
            Account account = new() {
                UserName = registerDTO.UserName,
                Password = registerDTO.Password
            };
            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();
            if (registerDTO.Role is "Student") {
                dbContext.Students.Add(new Student() {StudentId = account.AccountId});
                dbContext.SaveChanges();
            } else if (registerDTO.Role is "Trainer") {
                dbContext.Trainers.Add(new Trainer() {TrainerId = account.AccountId});
                dbContext.SaveChanges();
            }
            return account.AccountId;
        }

        public bool deleteAccount(int id)
        {
            Account account = dbContext.Accounts.Find(id);
            if (account is null) {
                return false;
            }
            dbContext.Accounts.Remove(account);
            dbContext.SaveChanges();
            return true;
        }
    }
}