using System;

namespace Library.Domain.Entities
{
    public abstract class IEntity
    {
        public Guid Id { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}