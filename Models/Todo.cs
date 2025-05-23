using System.ComponentModel.DataAnnotations;

namespace toDo.Models
{
  public class Todo : IValidatableObject
  {
    public int Id { get; set; }

    [StringLength(60, MinimumLength = 5, ErrorMessage = "The Title field should be at least 5 and a maximum of 100 characters long.")]
    [Required(ErrorMessage = "The Title field is obligatory.")]
    public string Title { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; }

    [Required(ErrorMessage = "The Deadline date is invalid.")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? Deadline { get; set; }

    public DateTime? CompletedAt { get; set; }

    public ApplicationUser? User { get; set; }

    public string UserId { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Deadline.HasValue && Deadline.Value.Date < DateTime.Now.Date)
      {
        yield return new ValidationResult("The Deadline date cannot be in the past.", new[] { nameof(Deadline) });
      }
    }

    public void Complete()
    {
      IsCompleted = !IsCompleted;
      CompletedAt = IsCompleted ? DateTime.Now : null;
    }
    
  }
}