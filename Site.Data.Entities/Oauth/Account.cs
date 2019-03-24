using Microsoft.AspNet.Identity;
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

        [NotMapped]
        public string UserName { get; set; }

        [DataType(DataType.Password), MaxLength(50)]
        public string Password { get; set; }

        [Required, MaxLength(330)]
        public string Email { get; set; }

        [Required, MaxLength(100)]
        public string PharmacistLicense { get; set; }

        [MaxLength(300)]
        public string PharmacySetting { get; set; }

        public int PharmacySettingId { get; set; }

        [MaxLength(300)]
        public string Specialty { get; set; }

        public int SpecialtyId { get; set; }

        [Required, MaxLength(300)]
        public string CountryIso2 { get; set; }

        [Required, MinLength(2), MaxLength(2)]
        public string ProvinceId { get; set; }

        [Required, MaxLength(300)]
        public string City { get; set; }

        public bool IsOptin { get; set; }
        public bool IsActivated { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed), DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
    }
}