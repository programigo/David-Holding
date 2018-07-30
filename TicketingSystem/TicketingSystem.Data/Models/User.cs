using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TicketingSystem.Common.Constants;

namespace TicketingSystem.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MinLength(DataConstants.UserNameMinLength)]
        [MaxLength(DataConstants.UserNameMaxLength)]
        public string Name { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        public bool IsApproved { get; set; }
    }
}
