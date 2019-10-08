using System;
using System.Collections.Generic;

namespace Palladin.Data.Entity
{
    public class CompanyEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool MasterCompany { get; set; }

        public ICollection<UserEntity> Users { get; set; }
    }
}
