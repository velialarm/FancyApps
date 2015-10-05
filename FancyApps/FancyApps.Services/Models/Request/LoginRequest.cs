namespace FancyApps.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string FanApp { get; set; }

    }
}