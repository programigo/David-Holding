using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TicketingSystem.Services;
using TicketingSystem.VueTS.Common.Models;
using TicketingSystem.Web.Models.ManageViewModels;

namespace TicketingSystem.VueTS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
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
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            var changePasswordReturnResult = new Microsoft.AspNetCore.Identity.IdentityResult
            {

            };

            if (!changePasswordResult.Succeeded)
            {
                IdentityResultWeb changePasswordRes = new IdentityResultWeb(changePasswordReturnResult);
                AddErrors(changePasswordRes);
                return BadRequest();
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return Ok();
        }

        private void AddErrors(IdentityResultWeb result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
