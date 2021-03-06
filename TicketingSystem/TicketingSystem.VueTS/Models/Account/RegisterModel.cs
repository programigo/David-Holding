﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TicketingSystem.Data.Constants;

namespace TicketingSystem.VueTS.Models.AccountViewModels
{
	public class RegisterModel
	{
		[JsonProperty("username")]
		[Required]
		[StringLength(50)]
		public string Username { get; set; }

		[JsonProperty("name")]
		[Required]
		[MinLength(DataConstants.UserNameMinLength, ErrorMessage = "The Name must be at least {1} characters long.")]
		[MaxLength(DataConstants.UserNameMaxLength, ErrorMessage = "The Name must be maximum {1} characters long.")]
		public string Name { get; set; }

		[JsonProperty("email")]
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[JsonProperty("password")]
		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[JsonProperty("confirmPassword")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		//[JsonProperty("isApproved")]
		//public bool IsApproved { get; set; }
	}
}
