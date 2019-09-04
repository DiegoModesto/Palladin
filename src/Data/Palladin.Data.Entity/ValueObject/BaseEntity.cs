using System;

namespace Palladin.Data.Entity
{
    public abstract class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
