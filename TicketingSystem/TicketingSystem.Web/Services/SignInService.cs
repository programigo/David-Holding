using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingSystem.Services;
using AuthenticationProperties = TicketingSystem.Services.AuthenticationProperties;
using AuthenticationScheme = TicketingSystem.Services.AuthenticationScheme;
using DATA_MODELS = TicketingSystem.Data.Models;
using ExternalLoginInfo = TicketingSystem.Services.ExternalLoginInfo;
using SignInResult = TicketingSystem.Services.SignInResult;

namespace TicketingSystem.Web.Services
{
    public class SignInService : SignInManager<DATA_MODELS.User>, ISignInService
    {
        public SignInService(UserManager<DATA_MODELS.User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<DATA_MODELS.User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<DATA_MODELS.User>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            var res = base.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            var returnRes = new AuthenticationProperties
            {
                IsPersistent = res.IsPersistent,
                RedirectUri = res.RedirectUri,
                Items = res.Items
            };

            return returnRes;
        }
        

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            var res = await base.GetExternalLoginInfoAsync();

            var returnRes = new ExternalLoginInfo(res.Principal, res.LoginProvider, res.ProviderKey, res.ProviderDisplayName);

            return returnRes;
        }

        public async Task SignInAsync(User signInUser, bool isPersistent)
        {
            DATA_MODELS.User returnUser = await UserManager.FindByIdAsync(signInUser.Id);
        }

        AuthenticationProperties ISignInService.ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId)
        {
            var res = base.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId);

            var returnRes = new AuthenticationProperties
            {
                IsPersistent = res.IsPersistent,
                RedirectUri = res.RedirectUri,
                Items = res.Items
            };

            return returnRes;
        }

        async Task<SignInResult> ISignInService.ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor)
        {
            var res = await base.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent, bypassTwoFactor);

            var returnRes = new SignInResult
            {
                Succeeded = res.Succeeded,
                IsLockedOut = res.IsLockedOut,
                IsNotAllowed = res.IsNotAllowed,
                RequiresTwoFactor = res.RequiresTwoFactor
            };

            return returnRes;
        }

        async Task<IEnumerable<AuthenticationScheme>> ISignInService.GetExternalAuthenticationSchemesAsync()
        {
            var res = await base.GetExternalAuthenticationSchemesAsync();

            var returnRes = new List<AuthenticationScheme>();

            foreach (var scheme in res)
            {
                var systemScheme = new AuthenticationScheme(scheme.Name, scheme.DisplayName, scheme.HandlerType);

                returnRes.Add(systemScheme);
            }

            return returnRes;
        }

        async Task<ExternalLoginInfo> ISignInService.GetExternalLoginInfoAsync(string id)
        {
            var res = await base.GetExternalLoginInfoAsync(id);

            var returnRes = new ExternalLoginInfo(res.Principal, res.LoginProvider, res.ProviderKey, res.ProviderDisplayName);

            return returnRes;
        }

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

        async Task<SignInResult> ISignInService.PasswordSignInAsync(string username, string password, bool rememberMe, bool lockoutOnFailure)
        {
            var res = await base.PasswordSignInAsync(username, password, rememberMe, lockoutOnFailure);

            var returnRes = new SignInResult
            {
                Succeeded = res.Succeeded,
                IsLockedOut = res.IsLockedOut,
                IsNotAllowed = res.IsNotAllowed,
                RequiresTwoFactor = res.RequiresTwoFactor
            };

            return returnRes;
        }

        async Task<SignInResult> ISignInService.TwoFactorAuthenticatorSignInAsync(string authenticatorCode, bool rememberMe, bool rememberMachine)
        {
            var res = await base.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, rememberMachine);

            var returnRes = new SignInResult
            {
                Succeeded = res.Succeeded,
                IsLockedOut = res.IsLockedOut,
                IsNotAllowed = res.IsNotAllowed,
                RequiresTwoFactor = res.RequiresTwoFactor
            };

            return returnRes;
        }

        async Task<SignInResult> ISignInService.TwoFactorRecoveryCodeSignInAsync(string recoveryCode)
        {
            var res = await base.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            var returnRes = new SignInResult
            {
                Succeeded = res.Succeeded,
                IsLockedOut = res.IsLockedOut,
                IsNotAllowed = res.IsNotAllowed,
                RequiresTwoFactor = res.RequiresTwoFactor
            };

            return returnRes;
        }
    }
}
