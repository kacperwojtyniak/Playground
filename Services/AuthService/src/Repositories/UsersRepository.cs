using AuthService.Interfaces;
using Database.Common.ContextProvider;
using Database.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IContextProvider _contextProvider;

        public UsersRepository(IContextProvider contextProvider)
        {
            this._contextProvider = contextProvider;
        }
        

        public async Task<UserModel> GetUserByLoginAsync(string login)
        {
            try
            {
                using (var context = _contextProvider.GetContext<UserModel>())
                {
                    return await context.GetSingleAsync(x => x.UserLogin == login);
                }
            }
            catch (Exception ex)
            {
                //#TODO log
                throw new ArgumentException("User not found");                
            }
            
        }
    }
}
