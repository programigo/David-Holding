using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.VueTS.Areas.Projects.Models.Users
{
    public class AddUserToRoleFormModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
