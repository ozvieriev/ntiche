using System.ComponentModel.DataAnnotations;

namespace Site.UI.Models
{
    public class RegisterViewModel
    {
        [Required, MaxLength(300)]
        public string FirstName { get; set; }

        [Required, MaxLength(300)]
        public string LastName { get; set; }

        [Required, MaxLength(300)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MaxLength(330), DataType(DataType.EmailAddress)]
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
    }
}