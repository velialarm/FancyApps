namespace FancyApps.Services.Models.Request
{
    using System.ComponentModel.DataAnnotations;

    public class RequestModel
    {
        [Required]
        public string Token { get; set; }
    }
}