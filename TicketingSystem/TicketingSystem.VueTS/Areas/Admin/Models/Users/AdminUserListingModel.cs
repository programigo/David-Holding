﻿namespace TicketingSystem.VueTS.Areas.Admin.Models.Users
{
    public class AdminUserListingModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsApproved { get; set; }
    }
}