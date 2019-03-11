using System.ComponentModel.DataAnnotations;

namespace Site.UI.Models
{
    public class RegisterViewModel
    {
        [Required, MaxLength(300)]
        public string FirstName { get; set; }

        [Required, MaxLength(300)]
        public string LastName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MaxLength(330), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, MaxLength(100)]
        public string PharmacistLicense { get; set; }

        public int PharmacySettingId { get; set; }

        [Required, MaxLength(300)]
        public string Ocupation { get; set; }

        [Required, MaxLength(300)]
        public string CountryIso2 { get; set; }

        [Required, MinLength(2), MaxLength(2)]
        public string ProvinceId { get; set; }

        [Required, MaxLength(300)]
        public string City { get; set; }

        public bool IsOptin { get; set; }
        public bool IsActivated { get; set; }
    }
}