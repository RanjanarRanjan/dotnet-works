using System.ComponentModel.DataAnnotations;

public class AdminUpdateUserDto
{
    [Required]
    [RegularExpression("^[0-9]{10}$",
        ErrorMessage = "Contact number must be exactly 10 digits")]
    public string ContactNumber { get; set; } = null!;

    [Required]
    public string Department { get; set; } = null!;
}
