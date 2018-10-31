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

namespace TicketingSystem.VueTS.Areas.Admin.Controllers
{
    [Authorize]
    [Route("api/users")]

    public class UsersController : ControllerBase
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
                .ProjectTo<AdminUserListingModel>()
                .ToArray();

            var roles = this.roleManager.GetRoles().ToArray();

            return Ok(new UserListingModel
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
                .ProjectTo<AdminUserListingModel>()
                .ToArray();

            return Ok(new UserPendingModel
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

                return Ok();
            }
            
            await this.userManager.AddToRoleAsync(user, model.Role);

            return Ok();
        }

        [HttpPost("approve/{id}")]
        public async Task<IActionResult> Approve(string id)
        {
            User user = await this.userManager.FindByIdAsync(id);

            this.users.Approve(id);

            return Ok();
        }

        [HttpGet("register")]
        public IActionResult Register(string returnUrl = null)
        {
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model, string returnUrl = null)
        {

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

                return Ok();
            }
           
            return BadRequest(model);
        }

        [HttpPost("remove/{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            User user = await this.userManager.FindByIdAsync(id);

            this.users.Remove(id);

            return Ok();
        }

        [HttpGet("changeuserdata/{id}")]
        public IActionResult ChangeUserData(string id)
        {
            UserChangeDataModel user = this.users.Details(id)
                .ProjectTo<UserChangeDataModel>().FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("changeuserdata/{id}")]
        public IActionResult ChangeUserData(string id, [FromBody]AdminChangeDataModel model)
        {
            bool changedUser = this.users.ChangeData(id, model.Name, model.Email);

            if (!changedUser)
            {
                return NotFound();
            }

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

            AdminUserChangePasswordModel model = new AdminUserChangePasswordModel();

            return Ok(model);
        }

        [HttpPut("changeuserpassword/{id}")]
        public async Task<ActionResult> ChangeUserPassword(string id, [FromBody]AdminUserChangePasswordModel model)
        {
            User user = this.users.GetUser(id).FirstOrDefault();

            IdentityResult result = await userManager.RemovePasswordAsync(user);

            if (result.Succeeded)
            {
                result = await userManager.AddPasswordAsync(user, model.NewPassword);
            }

            return Ok();
        }
    }
}