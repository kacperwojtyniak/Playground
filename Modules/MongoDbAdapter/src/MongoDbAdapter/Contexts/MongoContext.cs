using Database.Common.Configuration;
using Database.Common.Contexts;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbAdapterModule.Contexts
{
    public class MongoContext<T> : IMongoContext<T>
    {
        private readonly DbConfig _config;
        private  IMongoCollection<T> _collection;

        public MongoContext(DbConfig config)
        {
            this._config = config;
            var client = new MongoClient(_config.ConnectionString);
            this._collection = client.GetDatabase(_config.DbName).GetCollection<T>(typeof(T).Name);
        }
        
        public async Task AddAsync(T entity)
        {            
            await this._collection.InsertOneAsync(entity);
        }

        public async Task AddManyAsync(IEnumerable<T> entities)
        {
            await this._collection.InsertManyAsync(entities);
        }

        public async Task DeleteWhereAsync(Expression<Func<T, bool>> where)
        {
            await this._collection.DeleteOneAsync(Builders<T>.Filter.Where(where));            
        }        

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _collection.FindAsync(x => true);
            return await result.ToListAsync();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> where)
        {
            var result = await this._collection.FindAsync(where);
             return result.First();
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> where)
        {
            var result = await this._collection.FindAsync(where);
            return await result.ToListAsync();
        }

        public void Dispose()
        {
            this._collection = null;
        }
    }
}
