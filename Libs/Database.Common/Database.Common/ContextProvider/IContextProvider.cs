using Database.Common.Contexts;
using Database.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Common.ContextProvider
{
    public interface IContextProvider
    {
        IMongoContext<T> GetContext<T>() where T : BaseModel;
    }
}
