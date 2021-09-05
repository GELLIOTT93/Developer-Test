using _288.TechTest.Data.Entities;
using _288.TechTest.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace _288.TechTest.Data.Services
{
    /// <summary>
    /// Generic class that can be used against any entity inherits from entity base
    /// </summary>
    /// <typeparam name="TEntity">Will need to inherit from entity base</typeparam>
    /// <typeparam name="TContext">The db context that is being used</typeparam>
    /// <typeparam name="TIdentifier">The type of id for that entity</typeparam>
    public abstract class CrudRepo<TEntity, TContext, TIdentifier> : ICrudRepo<TEntity, TIdentifier>
        where TEntity : EntityBase<TIdentifier>
        where TContext : DbContext
    {
        private readonly TContext context;
        public CrudRepo(TContext context)
        {
            this.context = context;
        }

        /// <inheritdoc />
        public async Task<TEntity> Delete(TIdentifier id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        /// <inheritdoc />
        public async Task<TEntity> GetById(TIdentifier id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<TEntity> Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc />
        public async Task<TEntity> Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
