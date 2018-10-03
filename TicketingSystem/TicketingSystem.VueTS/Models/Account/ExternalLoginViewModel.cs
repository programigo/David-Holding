using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.VueTS.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
