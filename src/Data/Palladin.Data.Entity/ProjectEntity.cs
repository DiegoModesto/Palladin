using System;

namespace Palladin.Data.Entity
{
    public class ProjectEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
        public Enums.ProjType ProjectType { get; set; }
        public string Subsidiary { get; set; }

        public Guid? CustomerId { get; set; }
        public UserEntity Customer { get; set; }

        public Guid? UserId { get; set; }
        public UserEntity User { get; set; }

        public ProjectVulnerabilityEntity ProjectVulnerability { get; set; }
    }
}
