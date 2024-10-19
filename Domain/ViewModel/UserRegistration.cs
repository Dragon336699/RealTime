using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel
{
    public class UserRegistration
    {
        public String FirstName {  get; set; }
        public String LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public String Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public String ConfirmPassword { get; set; }
    }
}
