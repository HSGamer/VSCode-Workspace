using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRole.Models;
using TestRole.Models.DTO;

namespace TestRole.Services
{
    public interface IAccountManager
    {
        int addAccount(RegisterDTO registerDTO);

        bool deleteAccount(int id);
    }
}