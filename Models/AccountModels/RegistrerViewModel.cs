using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SIFCore.Models
{

    public class RegisterModel
    {        

        [Required]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{6,100}", ErrorMessage = "Your password must be at least 6 characters long and contain at least 1 letter and 1 number")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public Contacts Contact { get; set; }
    }

}