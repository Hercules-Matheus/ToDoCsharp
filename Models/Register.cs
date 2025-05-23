using System.ComponentModel.DataAnnotations;

namespace toDo.ViewModels
{
    public class RegisterViewModel
    {
      [Required]
      [EmailAddress]
      public string Email { get; set; } = string.Empty;

      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; } = string.Empty;

      [Required]
      [DataType(DataType.Password)]
      [Compare("Password", ErrorMessage = "Password not match.")]
      public string ConfirmPassword { get; set; } = string.Empty;
    }
}
