using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TicketingSystem.Services
{
    public interface IUserService
    {
        IdentityOptionsModel Options { get; set; }

        Task<Microsoft.AspNetCore.Identity.IdentityRole> GetUserRole(string id);

        Task<User> FindByIdAsync(string userId);

        Task<IdentityResult> AddToRoleAsync(User user, string role);

        Task<IdentityResult> CreateAsync(User user, string password);

        Task<IdentityResult> CreateAsync(User user);

        string GetUserId(ClaimsPrincipal user);

        string GetUserName(ClaimsPrincipal user);

        Task<bool> HasPasswordAsync(User user);

        Task<IdentityResult> RemovePasswordAsync(User user);

        Task<IdentityResult> AddPasswordAsync(User user, string newPassword);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> AddLoginAsync(User user, ExternalLoginInfo info);

        Task<IdentityResult> ConfirmEmailAsync(User user, string code);

        Task<User> FindByEmailAsync(string email);

        Task<bool> IsEmailConfirmedAsync(User user);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string code, string password);

        Task<User> GetUserAsync(ClaimsPrincipal user);

        Task<IdentityResult> SetEmailAsync(User user, string email);

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        Task<IList<UserLoginInfo>> GetLoginsAsync(User user);

        Task<IdentityResult> RemoveLoginAsync(User user, string loginProvider, string providerKey);

        Task<string> GetAuthenticatorKeyAsync(User user);

        Task<int> CountRecoveryCodesAsync(User user);

        Task<IdentityResult> SetTwoFactorEnabledAsync(User user, bool enabled);

        Task<bool> VerifyTwoFactorTokenAsync(User user, string authenticatorTokenProvider, string verificationCode);

        Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(User user, int number);

        Task<IdentityResult> ResetAuthenticatorKeyAsync(User user);
    }
}
