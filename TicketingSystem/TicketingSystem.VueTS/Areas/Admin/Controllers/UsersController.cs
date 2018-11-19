using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem.Services;
using TicketingSystem.VueTS.Areas.Admin.Models.Users;
using TicketingSystem.VueTS.Common.Constants;
using TicketingSystem.VueTS.Models;
using TicketingSystem.VueTS.Models.AccountViewModels;
using IdentityResult = TicketingSystem.Services.IdentityResult;

namespace TicketingSystem.VueTS.Areas.Admin.Controllers
{
	[Authorize(Roles = WebConstants.AdministratorRole)]
	[Route("api/users")]

	public class UsersController : ControllerBase
	{
		private readonly IAdminUserService users;
		private readonly IUserService userManager;
		private readonly IRoleService roleManager;

		public UsersController(IAdminUserService users, IUserService userManager, IRoleService roleManager)
		{
			this.users = users ?? throw new ArgumentNullException(nameof(users));
			this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
		}

		[HttpGet]
		public IActionResult Index()
		{
			AdminUserListingModel[] users = this.users.GetAllUsers()
				.ProjectTo<AdminUserListingModel>()
				.ToArray();

			SelectListItem[] roles = this.roleManager.GetRoles().ToArray();

			var result = new UserListingModel
			{
				Users = users,
				Roles = roles.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
				{
					Text = r.Text,
					Value = r.Value
				})
			};

			return Ok(result);
		}

		[HttpGet("pending")]
		public IActionResult Pending()
		{
			AdminUserListingModel[] users = this.users.GetPendingUsers()
				.ProjectTo<AdminUserListingModel>()
				.ToArray();

			var result = new UserPendingModel
			{
				Users = users
			};

			return Ok(result);
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
		public async Task<IActionResult> Approve([FromRoute(Name = "id")] string id)
		{
			User user = await this.userManager.FindByIdAsync(id);

			if (user == null)
			{
				return NotFound("No such user exists.");
			}

			this.users.Approve(id);

			return Ok();
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody]RegisterModel model, string returnUrl = null)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState.ToBadRequestErrorModel());
			}

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

		[HttpPost("remove/{id}")]
		public async Task<IActionResult> Remove([FromRoute(Name = "id")] string id)
		{
			User user = await this.userManager.FindByIdAsync(id);

			if (user == null)
			{
				return NotFound("No such user exists.");
			}

			this.users.Remove(id);

			return Ok();
		}

		[HttpGet("changeuserdata/{id}")]
		public IActionResult ChangeUserData([FromRoute(Name = "id")] string id)
		{
			UserChangeDataModel user = this.users.Details(id)
				.ProjectTo<UserChangeDataModel>().FirstOrDefault();

			if (user == null)
			{
				return NotFound("No such user exists.");
			}

			return Ok(user);
		}

		[HttpPut("changeuserdata/{id}")]
		public IActionResult ChangeUserData([FromRoute(Name = "id")] string id, [FromBody]AdminChangeDataModel model)
		{
			bool changedUser = this.users.ChangeData(id, model.Name, model.Email);

			if (!changedUser)
			{
				return NotFound();
			}

			return Ok();
		}

		[HttpPut("changeuserpassword/{id}")]
		public async Task<ActionResult> ChangeUserPassword([FromRoute(Name = "id")] string id, [FromBody]AdminUserChangePasswordModel model)
		{
			User user = this.users.GetUser(id).FirstOrDefault();

			if (user == null)
			{
				return NotFound("No such user exists.");
			}

			IdentityResult result = await userManager.RemovePasswordAsync(user);

			if (result.Succeeded)
			{
				result = await userManager.AddPasswordAsync(user, model.NewPassword);
			}

			return Ok();
		}
	}
}