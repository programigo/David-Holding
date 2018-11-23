using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TicketingSystem.Services;
using TicketingSystem.VueTS.Models;
using TicketingSystem.Web.Models.ManageViewModels;

namespace TicketingSystem.VueTS.Controllers
{
	[Authorize]
	[Route("api/manage")]
	public class ManageController : Controller
	{
		private readonly IUserService _userManager;
		private readonly ISignInService _signInManager;
		private readonly ILogger _logger;

		public ManageController(
		  IUserService userManager,
		  ISignInService signInManager,
		  ILogger<ManageController> logger,
		  UrlEncoder urlEncoder)
		{
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[TempData]
		public string StatusMessage { get; set; }

		[AllowAnonymous]
		[HttpPut("changepassword")]
		public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState.ToBadRequestErrorModel());
			}

			User user = await _userManager.GetUserAsync(User);

			if (user == null)
			{
				return NotFound("No such user exists.");
			}

			var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

			if (!changePasswordResult.Succeeded)
			{
				ModelState.AddModelError(string.Empty, "The old password is wrong.");
				return BadRequest(ModelState.ToBadRequestErrorModel());
			}

			await _signInManager.SignInAsync(user, isPersistent: false);
			_logger.LogInformation("User changed their password successfully.");
			StatusMessage = "Your password has been changed.";

			return Ok();
		}
	}
}
