using System.ComponentModel.DataAnnotations;

namespace BuiTienQuatMVC.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email not empty")]
        [EmailAddress(ErrorMessage = "Email no valid")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "password not empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
