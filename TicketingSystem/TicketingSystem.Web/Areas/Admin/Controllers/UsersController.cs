﻿using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATA_MODELS = TicketingSystem.Data.Models;
using TicketingSystem.Services;
using TicketingSystem.Web.Areas.Admin.Models.Users;
using TicketingSystem.Web.Areas.Projects.Models.Users;
using TicketingSystem.Web.Infrastructure.Extensions;
using TicketingSystem.Web.Models.AccountViewModels;

namespace TicketingSystem.Web.Areas.Projects.Controllers
{
    public class UsersController : BaseAdminController
    {
        private readonly IAdminUserService users;
        private readonly UserManager<DATA_MODELS.User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(IAdminUserService users, UserManager<DATA_MODELS.User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.users = users;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            List<AdminUserListingViewModel> users = this.users.GetAllUsers()
                .ProjectTo<AdminUserListingViewModel>().ToList();

            var roles = this.roleManager.GetRoles();

            return View(new UserListingViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        public IActionResult Pending()
        {
            List<AdminUserListingViewModel> users = this.users.GetPendingUsers()
                .ProjectTo<AdminUserListingViewModel>().ToList();

            return View(new UserPendingViewModel
            {
                Users = users
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleFormModel model)
        {
            bool roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            DATA_MODELS.User user = await this.userManager.FindByIdAsync(model.UserId);
            bool userExists = user != null;

            bool isAlreadyInRole = this.users.IsAlreadyInRole(user.Id);

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!isAlreadyInRole)
            {
                await this.userManager.AddToRoleAsync(user, model.Role);

                TempData.AddSuccessMessage($"User {user.UserName} successfully added to {model.Role} role.");

                return RedirectToAction(nameof(Index));
            }
            
            await this.userManager.AddToRoleAsync(user, model.Role);

            TempData.AddSuccessMessage($"User {user.UserName} successfully added to {model.Role} role.");

            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Approve(string id)
        {
            DATA_MODELS.User user = await this.userManager.FindByIdAsync(id);

            this.users.Approve(id);

            TempData.AddSuccessMessage($"User {user.UserName} successfully approved.");

            return RedirectToAction(nameof(Pending));
        }

        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                DATA_MODELS.User user = new DATA_MODELS.User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Name = model.Name,
                    IsApproved = true
                };

                await userManager.CreateAsync(user, model.Password);

                TempData.AddSuccessMessage($"User {user.UserName} successfully created");

                return RedirectToAction(nameof(Index));
            }
           
            return View(model);
        }

        
        public async Task<IActionResult> Remove(string id)
        {
            DATA_MODELS.User user = await this.userManager.FindByIdAsync(id);

            this.users.Remove(id);

            TempData.AddSuccessMessage($"User {user.UserName} successfully removed");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangeUserData(string id)
        {
            UserChangeDataViewModel user = this.users.Details(id)
                .ProjectTo<UserChangeDataViewModel>().FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult ChangeUserData(string id, AdminChangeDataViewModel model)
        {
            bool changedUser = this.users.ChangeData(id, model.Name, model.Email);

            if (!changedUser)
            {
                return NotFound();
            }

            TempData.AddSuccessMessage($"User data for {model.Name} changed successfuly.");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangeUserPassword(string id)
        {
            DATA_MODELS.User user = this.users.GetUser(id)
                .Select(u => new DATA_MODELS.User
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Name = u.Name,
                    Email = u.Email,
                    IsApproved = u.IsApproved
                })
                .FirstOrDefault();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            bool hasPassword = await userManager.HasPasswordAsync(user);

            AdminUserChangePasswordViewModel model = new AdminUserChangePasswordViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeUserPassword(string id, AdminUserChangePasswordViewModel model)
        {
            DATA_MODELS.User user = this.users.GetUser(id)
               .Select(u => new DATA_MODELS.User
               {
                   Id = u.Id,
                   UserName = u.UserName,
                   Name = u.Name,
                   Email = u.Email,
                   IsApproved = u.IsApproved
               })
               .FirstOrDefault();

            IdentityResult result = await userManager.RemovePasswordAsync(user);

            if (result.Succeeded)
            {
                result = await userManager.AddPasswordAsync(user, model.NewPassword);
            }

            TempData.AddSuccessMessage($"Password for {user.UserName} changed successfuly.");

            return RedirectToAction(nameof(Index));
        }
    }
}