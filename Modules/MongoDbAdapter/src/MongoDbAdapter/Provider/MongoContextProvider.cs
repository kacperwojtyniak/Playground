using Database.Common.ContextProvider;
using Database.Common.Contexts;
using Database.Common.Models;
using Demos.Common;
using MongoDbAdapterModule.Contexts;
using System;

namespace MongoDbAdapterModule.Provider
{
    public class MongoContextProvider : IContextProvider
    {
        public IMongoContext<T> GetContext<T>() where T : BaseModel
        {
            return IoC.Resolve<IMongoContext<T>>();
        }
    }
}
