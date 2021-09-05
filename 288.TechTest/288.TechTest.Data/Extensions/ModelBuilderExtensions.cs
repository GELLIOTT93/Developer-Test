using _288.TechTest.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace _288.TechTest.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Generic method 
        /// </summary>
        /// <typeparam name="TEntity">Thhe entity type we are applying this to.</typeparam>
        /// <typeparam name="TIdentifier">You can define the id type on entity base so this needs to be passed in.</typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static EntityTypeBuilder<TEntity> ExcludeSoftDeleted<TEntity, TIdentifier>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : EntityBase<TIdentifier>
        {
            return builder.HasQueryFilter(x => !x.DeletedDate.HasValue);
        }
    }
}
