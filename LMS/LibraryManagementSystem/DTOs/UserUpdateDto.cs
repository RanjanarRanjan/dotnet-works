using System.ComponentModel.DataAnnotations;

public class UserUpdateDto
{
    [Required]
    [RegularExpression("^[0-9]{10}$",
        ErrorMessage = "Contact number must be exactly 10 digits")]
    public string ContactNumber { get; set; } = null!;

    [Required]
    public string Department { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
