using System;
using System.Collections.Generic;

namespace Palladin.Data.Entity
{
    public class MediaEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name{ get; set; }
        public string Archive { get; set; }

        public IEnumerable<MediaPVEntity> MediaPVEntities { get; set; }
    }
}
