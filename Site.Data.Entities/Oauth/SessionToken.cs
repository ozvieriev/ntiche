using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Data.Entities.Oauth
{
    public class SessionToken : Entity<Guid>
    {
        public Guid AccountId { get; set; }

        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }

        [Required]
        public string ProtectedTicket { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateDateUtc { get; set; }
    }
}
