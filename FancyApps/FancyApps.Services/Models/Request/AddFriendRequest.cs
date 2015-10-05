namespace FancyApps.Services.Models.Request
{
    using System.ComponentModel.DataAnnotations;
    using Model;

    public class AddFriendRequest : RequestModel
    {
        public User User { get; set; }

        public string Nickname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}