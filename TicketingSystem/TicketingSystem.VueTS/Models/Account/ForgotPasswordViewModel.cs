﻿using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.VueTS.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}