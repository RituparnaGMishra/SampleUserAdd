using SampleUserAdd.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace SampleUserAdd.Models
{
    public class UserEntityModel
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
              ErrorMessage = "Not a valid email id")]
        public string Email { get; set; } = null!;
        [PasswordValidation]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
