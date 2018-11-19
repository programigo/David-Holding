using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketingSystem.Services
{
	public interface ISignInService
	{
		Task<SignInResult> PasswordSignInAsync(string username, string password, bool lockoutOnFailure);

		Task<User> GetTwoFactorAuthenticationUserAsync();

		Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string authenticatorCode, bool rememberMe, bool rememberMachine);

		Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode);

		Task SignInAsync(User signInUser, bool isPersistent);

		Task SignOutAsync();

		bool IsSignedIn();

		AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);

		Task<ExternalLoginInfo> GetExternalLoginInfoAsync();

		Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor);

		Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();

		AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId);

		Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string id);
	}
}