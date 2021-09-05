using System;

namespace _288.TechTest.Data.Entities
{
    /// <summary>
    /// Should more often than not be applied to all database tables
    /// </summary>
    /// <typeparam name="TIdentifier">This will be used to declare what type id variable will be</typeparam>
    public class EntityBase<TIdentifier>
    {
        public virtual TIdentifier Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public virtual DateTime? DeletedDate { get; set; }
    }
}
