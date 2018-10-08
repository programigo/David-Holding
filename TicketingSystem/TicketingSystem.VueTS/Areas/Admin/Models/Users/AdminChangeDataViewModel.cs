using System.ComponentModel.DataAnnotations;
using TicketingSystem.Data.Constants;

namespace TicketingSystem.VueTS.Areas.Admin.Models.Users
{
    public class AdminChangeDataViewModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(DataConstants.UserNameMinLength)]
        [MaxLength(DataConstants.UserNameMaxLength)]
        public string Username { get; set; }

        [Required]
        [MinLength(DataConstants.UserNameMinLength)]
        [MaxLength(DataConstants.UserNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
