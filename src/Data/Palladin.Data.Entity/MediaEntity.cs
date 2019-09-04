using System;
using System.Collections.Generic;

namespace Palladin.Data.Entity
{
    public class MediaEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public byte[] Archive { get; set; }

        public IEnumerable<MediaPVEntity> MediaPVEntities { get; set; }
    }
}
