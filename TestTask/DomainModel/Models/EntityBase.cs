using System;

namespace DomainModel.Models
{
    public abstract class EntityBase 
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
