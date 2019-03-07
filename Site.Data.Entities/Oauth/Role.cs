﻿using Microsoft.AspNet.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site.Data.Entities.Oauth
{
    public class Account : Entity<Guid>, IUser<Guid>
    {
        [Required, MaxLength(300)]
        public string FirstName { get; set; }

        [Required, MaxLength(300)]
        public string LastName { get; set; }

        [Required, MaxLength(300)]
        public string UserName { get; set; }

        [DataType(DataType.Password), MaxLength(50)]
        public string Password { get; set; }

        [Required, MaxLength(330)]
        public string Email { get; set; }

        [Required, MaxLength(300)]
        public string Ocupation { get; set; }

        [Required, MaxLength(300)]
        public string CountryIso2 { get; set; }

        [Required, MaxLength(300)]
        public string Province { get; set; }

        [Required, MaxLength(300)]
        public string City { get; set; }

        public bool IsOptin { get; set; }
        public bool IsActivated { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed), DataType(DataType.DateTime)]
        public DateTime CreateDateUtc { get; set; }
    }
}