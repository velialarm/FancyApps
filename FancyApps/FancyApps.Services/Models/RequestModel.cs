namespace FancyApps.Services.Models.Request
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class RequestModel
    {
        [Required]
        public string Token { get; set; }

    }
}