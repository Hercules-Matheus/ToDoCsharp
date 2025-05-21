using System.ComponentModel.DataAnnotations;

namespace toDo.Models
{
  public class Todo : IValidatableObject
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "The Title field is obligatory.")]
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }

    [Required(ErrorMessage = "The Deadline date is invalid.")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateOnly? Deadline { get; set; }
    public DateOnly? CompletedAt { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Deadline.HasValue && Deadline.Value < DateOnly.FromDateTime(DateTime.Now))
      {
        yield return new ValidationResult("The Deadline date cannot be in the past.", [nameof(Deadline)]);
      }
    }
  }
}