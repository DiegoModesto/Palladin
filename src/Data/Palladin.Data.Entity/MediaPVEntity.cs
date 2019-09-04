using System;

namespace Palladin.Data.Entity
{
    public class MediaPVEntity : BaseEntity
    {
        public Guid MediaId { get; set; }
        public MediaEntity Media { get; set; }
        public Guid ProjectVulnerabilityId { get; set; }
        public ProjectVulnerabilityEntity ProjectVulnerability { get; set; }
    }
}
