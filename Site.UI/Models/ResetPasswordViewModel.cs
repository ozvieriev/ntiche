using System;
using System.ComponentModel.DataAnnotations;

namespace Site.UI.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string ResetPasswordToken { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}