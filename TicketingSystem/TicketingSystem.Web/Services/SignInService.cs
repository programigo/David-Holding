using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TicketingSystem.Services;
using DATA_MODELS = TicketingSystem.Data.Models;

namespace TicketingSystem.Web.Services
{
    public class SignInService : SignInManager<DATA_MODELS.User>, ISignInService
    {
        public SignInService(UserManager<DATA_MODELS.User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<DATA_MODELS.User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<DATA_MODELS.User>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        => base.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        public Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        => base.GetExternalLoginInfoAsync();

        public async Task SignInAsync(User signInUser, bool isPersistent)
        {
            DATA_MODELS.User returnUser = await UserManager.FindByIdAsync(signInUser.Id);
        }

        AuthenticationProperties ISignInService.ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId)
        => base.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId);

        Task<SignInResult> ISignInService.ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor)
        => base.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent, bypassTwoFactor);

        Task<IEnumerable<AuthenticationScheme>> ISignInService.GetExternalAuthenticationSchemesAsync()
        => base.GetExternalAuthenticationSchemesAsync();

        Task<ExternalLoginInfo> ISignInService.GetExternalLoginInfoAsync(string id)
        => base.GetExternalLoginInfoAsync(id);

        async Task<User> ISignInService.GetTwoFactorAuthenticationUserAsync()
        {
            var dataUser = await base.GetTwoFactorAuthenticationUserAsync();

            var returnUser = new User
            {
                Id = dataUser.Id,
                UserName = dataUser.UserName,
                Name = dataUser.Name,
                Email = dataUser.Email,
                IsApproved = dataUser.IsApproved,
                SecurityStamp = dataUser.SecurityStamp
            };

            return returnUser;
        }

        Task<SignInResult> ISignInService.PasswordSignInAsync(string username, string password, bool rememberMe, bool lockoutOnFailure)
        => base.PasswordSignInAsync(username, password, rememberMe, lockoutOnFailure);

        Task<SignInResult> ISignInService.TwoFactorAuthenticatorSignInAsync(string authenticatorCode, bool rememberMe, bool rememberMachine)
        => base.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, rememberMachine);

        Task<SignInResult> ISignInService.TwoFactorRecoveryCodeSignInAsync(string recoveryCode)
        => base.TwoFactorRecoveryCodeSignInAsync(recoveryCode);
    }
}
