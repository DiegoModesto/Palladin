using System;

namespace Palladin.Data.Entity
{
    public class TokenEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
