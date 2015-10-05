namespace FancyApps.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class SignUpRequestModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        [Display(Name = "firstName")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        [Display(Name = "lastName")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "email")]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Phone]
        [Display(Name = "phone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "fannApp")]
        public string FanApp { get; set; }

        [Display(Name = "gender")]
        public string Gender { get; set; }

        [Display(Name = "city")]
        public string City { get; set; }

        [Display(Name = "address")]
        public string Address { get; set; }
    }
}