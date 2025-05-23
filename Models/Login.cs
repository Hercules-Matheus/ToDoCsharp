using System.ComponentModel.DataAnnotations;

namespace toDo.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember login?")]
        public bool RememberMe { get; set; } = false;
    }
}
