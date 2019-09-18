using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Palladin.Data.Entity
{
    public class ProjectEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
        public Enums.ProjType ProjectType { get; set; }
        public string Subsidiary { get; set; }

        public Guid? CustomerId { get; set; }
        [NotMapped]
        public virtual UserEntity Customer { get; set; }

        public Guid? UserId { get; set; }
        [NotMapped]
        public virtual UserEntity User { get; set; }
    }
}
