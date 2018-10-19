using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketingSystem.Services;
using TicketingSystem.VueTS.Common.Constants;
using TicketingSystem.VueTS.Common.Models;
using TicketingSystem.VueTS.Models.AccountViewModels;
using DATA_MODELS = TicketingSystem.Data.Models;

namespace TicketingSystem.VueTS.Controllers
{
    [Authorize]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userManager;
        private readonly ISignInService _signInManager;
        private readonly ILogger _logger;
        private readonly IAdminUserService _users;

        public AccountController(
            IUserService userManager,
            ISignInService signInManager,
            ILogger<AccountController> logger,
            IAdminUserService users)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _users = users;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                bool isApprovedUser = _users.IsApprovedUser(model.Username);

                DATA_MODELS.User user = _users
                    .GetUserByName(model.Username)
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
                    ModelState.AddModelError(string.Empty, "No such user exists.");
                    return BadRequest(model);
                }
                if (isApprovedUser == false)
                {
                    ModelState.AddModelError(string.Empty, "You must wait to be approved by administrator.");
                    return BadRequest(model);
                }
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded && isApprovedUser)
                {
                    _logger.LogInformation("User logged in.");
                    return Ok(user);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return Ok(model);
        }

        [Authorize]
        [HttpGet("isLoggedOn")]
        public IActionResult IsLoggedOn()
        {
            
                //bool isLoggedOn = _signInManager.IsSignedIn();
                if (User.Identity.IsAuthenticated)
                {
                    return Ok();
                }

                return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Name = model.Name
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                var returnResult = new Microsoft.AspNetCore.Identity.IdentityResult
                {

                };
                if (result.Succeeded && user.IsApproved)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var signInUser = new User
                    {
                        UserName = model.Username,
                        Email = model.Email,
                        Name = model.Name
                    };

                    await _signInManager.SignInAsync(signInUser, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return Ok();
                }
                else
                {
                    IdentityResultWeb res = new IdentityResultWeb(returnResult);
                    return Ok();
                }
            }

            // If we got this far, something failed, redisplay form
            return Ok(model);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Ok();
        }
    }
}
