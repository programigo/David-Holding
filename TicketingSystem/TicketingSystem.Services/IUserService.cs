using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TicketingSystem.Services
{
    public interface IUserService
    {
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
    }
}
