using System;

namespace Palladin.Data.Entity
{
    public class MethodProtocolEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Enums.ProjType ProjectType { get; set; }
    }
}
