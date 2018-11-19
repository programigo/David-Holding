using System.ComponentModel.DataAnnotations;
using TicketingSystem.Data.Constants;

namespace TicketingSystem.Web.Models.ManageViewModels
{
	public class IndexViewModel
	{
		public string Username { get; set; }

		[MinLength(DataConstants.UserNameMinLength)]
		[MaxLength(DataConstants.UserNameMaxLength)]
		public string Name { get; set; }

		public bool IsEmailConfirmed { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Phone]
		[Display(Name = "Phone number")]
		public string PhoneNumber { get; set; }

		public string StatusMessage { get; set; }
	}
}
