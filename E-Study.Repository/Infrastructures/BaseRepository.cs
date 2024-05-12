using E_Study.Core;
using E_Study.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Study.Repository.Infrastructures
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
          where TEntity : class
    {
        protected readonly AppDbContext dataContext;
        protected DbSet<TEntity> dbSet;

        public BaseRepository(AppDbContext context)
        {
            dataContext = context;
            dbSet = context.Set<TEntity>();
        }
        public void Create(TEntity entity)
        {
            //_context.Entry<TEntity>(entity);
            dbSet.Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            //_context.Entry<TEntity>(entity);
            await dbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            // Context.Entry<TEntity>(entity).State = EntityState.Deleted;
        }

        public void Delete(string id)
        {
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }

        public TEntity GetById(params object[] primaryKey)
        {
            return dbSet.Find(primaryKey);
        }

        public async Task<TEntity> GetByIdAsync(params object[] primaryKey)
        {
            //_context.Entry<TEntity>(entity);
            return await dbSet.FindAsync(primaryKey);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet;
        }
        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
            // Context.Entry<TEntity>(entity).State = EntityState.Modified;
        }
    }
}
