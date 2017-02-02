using System;

namespace Livit.Model.Entities
{
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
