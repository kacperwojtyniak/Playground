using Database.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Interfaces
{
    public interface IUsersRepository
    {
         Task<UserModel> GetUserByLoginAsync(string login);
    }
}
