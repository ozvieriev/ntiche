using System.ComponentModel.DataAnnotations;

namespace Site.UI.Models
{
    public class RecoverPasswordViewModel
    {
        [Required, MaxLength(330), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}