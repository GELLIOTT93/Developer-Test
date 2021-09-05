using _288.TechTest.Data.Entities;
using System.Threading.Tasks;

namespace _288.TechTest.Data.Interfaces
{
    public interface ICrudRepo<TEntity, TIdentifier> where TEntity : EntityBase<TIdentifier>
    {
        /// <summary>
        /// Generic get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="TEntity"/></returns>
        Task<TEntity> GetById(TIdentifier id);
        /// <summary>
        /// Generic insert method
        /// </summary>
        /// <param name="entity"></param>
        /// <returns><see cref="TEntity"/></returns>
        Task<TEntity> Insert(TEntity entity);
        /// <summary>
        /// Generic update method
        /// </summary>
        /// <param name="entity"></param>
        /// <returns><see cref="TEntity"/></returns>
        Task<TEntity> Update(TEntity entity);
        /// <summary>
        /// Generic delete method
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="TEntity"/></returns>
        Task<TEntity> Delete(TIdentifier id);
    }
}
