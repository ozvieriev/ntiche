using Microsoft.AspNet.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site.Data.Entities.Oauth
{
    public class Account : Entity<Guid>, IUser<Guid>
    {
        [Required, MaxLength(330)]
        public string Email { get; set; }

        [DataType(DataType.Password), MaxLength(50)]
        public string Password { get; set; }

        public bool IsActivated { get; set; }

        [NotMapped]
        public string UserName
        {
            get { return string.Empty; }
            set { }
        }

        [DataType(DataType.DateTime)]
        public DateTime CreateDateUtc { get; set; }
    }
}
