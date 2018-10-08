using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem.Services;
using TicketingSystem.VueTS.Areas.Admin.Models.Users;
using TicketingSystem.VueTS.Common.Constants;
using TicketingSystem.VueTS.Infrastructure.Extensions;
using TicketingSystem.VueTS.Models.AccountViewModels;
using IdentityResult = TicketingSystem.Services.IdentityResult;

namespace TicketingSystem.VueTS.Controllers
{
    //[Authorize(Roles = WebConstants.AdministratorRole)]
    [Route("api/[controller]")]

    public class UsersController : Controller
    {
        private readonly IAdminUserService users;
        private readonly IUserService userManager;
        private readonly IRoleService roleManager;

        public UsersController(IAdminUserService users, IUserService userManager, IRoleService roleManager)
        {
            this.users = users;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = this.users.GetAllUsers()
                .ProjectTo<AdminUserListingViewModel>()
                .ToList();

            var roles = this.roleManager.GetRoles();

            return Ok(new UserListingViewModel
            {
                Users = users,
                Roles = roles.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = r.Text,
                    Value = r.Value
                })
            });
        }

        [HttpGet("pending")]
        public IActionResult Pending()
        {
            var users = this.users.GetPendingUsers()
                .ProjectTo<AdminUserListingViewModel>()
                .ToList();

            return Ok(new UserPendingViewModel
            {
                Users = users
            });
        }

        [HttpPost("addtorole")]
        public async Task<IActionResult> AddToRole([FromBody]AddUserToRoleFormModel model)
        {
            bool roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            User user = await this.userManager.FindByIdAsync(model.UserId);
            bool userExists = user != null;

            bool isAlreadyInRole = this.users.IsAlreadyInRole(user.Id);

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!isAlreadyInRole)
            {
                await this.userManager.AddToRoleAsync(user, model.Role);

                TempData.AddSuccessMessage($"User {user.UserName} successfully added to {model.Role} role.");

                return Ok();
            }
            
            await this.userManager.AddToRoleAsync(user, model.Role);

            TempData.AddSuccessMessage($"User {user.UserName} successfully added to {model.Role} role.");

            return Ok();
        }

        [HttpPost("approve/{id}")]
        public async Task<IActionResult> Approve(string id)
        {
            User user = await this.userManager.FindByIdAsync(id);

            this.users.Approve(id);

            TempData.AddSuccessMessage($"User {user.UserName} successfully approved.");

            return Ok();
        }

        [HttpGet("register")]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Name = model.Name,
                    IsApproved = true
                };

                await userManager.CreateAsync(user, model.Password);

                TempData.AddSuccessMessage($"User {user.UserName} successfully created");

                return Ok();
            }
           
            return Ok(model);
        }

        [HttpPost("remove/{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            User user = await this.userManager.FindByIdAsync(id);

            this.users.Remove(id);

            TempData.AddSuccessMessage($"User {user.UserName} successfully removed");

            return Ok();
        }

        [HttpGet("changeuserdata/{id}")]
        public IActionResult ChangeUserData(string id)
        {
            UserChangeDataViewModel user = this.users.Details(id)
                .ProjectTo<UserChangeDataViewModel>().FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("changeuserdata/{id}")]
        public IActionResult ChangeUserData(string id, [FromBody]AdminChangeDataViewModel model)
        {
            bool changedUser = this.users.ChangeData(id, model.Name, model.Email);

            if (!changedUser)
            {
                return NotFound();
            }

            TempData.AddSuccessMessage($"User data for {model.Name} changed successfuly.");

            return Ok();
        }

        [HttpGet("changeuserpassword/{id}")]
        public async Task<IActionResult> ChangeUserPassword(string id)
        {
            User user = this.users.GetUser(id).FirstOrDefault();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            bool hasPassword = await userManager.HasPasswordAsync(user);

            AdminUserChangePasswordViewModel model = new AdminUserChangePasswordViewModel();

            return Ok(model);
        }

        [HttpPost("changeuserpassword/{id}")]
        public async Task<ActionResult> ChangeUserPassword(string id, [FromBody]AdminUserChangePasswordViewModel model)
        {
            User user = this.users.GetUser(id).FirstOrDefault();

            IdentityResult result = await userManager.RemovePasswordAsync(user);

            if (result.Succeeded)
            {
                result = await userManager.AddPasswordAsync(user, model.NewPassword);
            }

            TempData.AddSuccessMessage($"Password for {user.UserName} changed successfuly.");

            return Ok();
        }
    }
}