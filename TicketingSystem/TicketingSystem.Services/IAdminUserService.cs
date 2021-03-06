﻿using System.Collections.Generic;
using System.Linq;

namespace TicketingSystem.Services
{
	public interface IAdminUserService
	{
		IEnumerable<AdminUserListingServiceModel> All();

		IQueryable<AdminUserListingServiceModel> GetAllUsers();

		IQueryable<AdminUserListingServiceModel> GetPendingUsers();

		IQueryable<AdminUserChangeDataServiceModel> Details(string id);

		IQueryable<User> GetUser(string id);

		IQueryable<User> GetUserByName(string username);

		void Approve(string id);

		void Remove(string id);

		bool IsApprovedUser(string username);

		bool ChangeData(string id, string name, string email);

		bool IsAlreadyInRole(string id);
	}
}
