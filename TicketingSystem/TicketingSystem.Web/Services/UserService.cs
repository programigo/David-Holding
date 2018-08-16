using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketingSystem.Services;
using TicketingSystem.Web.Infrastructure.Extensions;
using DATA_MODELS = TicketingSystem.Data.Models;

namespace TicketingSystem.Web.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<DATA_MODELS.User> userManager;

        public UserService(UserManager<DATA_MODELS.User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> AddLoginAsync(User user, ExternalLoginInfo info)
        {
            DATA_MODELS.User returnUser = new DATA_MODELS.User
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                IsApproved = user.IsApproved,
                SecurityStamp = user.SecurityStamp
            };

            return await userManager.AddLoginAsync(returnUser, info);
        }

        public async Task<IdentityResult> AddPasswordAsync(User user, string password)
        {
            DATA_MODELS.User returnUser = await userManager.FindByIdAsync(user.Id);

            return await userManager.AddPasswordAsync(returnUser, password);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            DATA_MODELS.User returnUser = await userManager.FindByIdAsync(user.Id);

            return await userManager.AddToRoleAsync(returnUser, role);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            DATA_MODELS.User returnUser = await userManager.FindByIdAsync(user.Id);

            return await userManager.ChangePasswordAsync(returnUser, oldPassword, newPassword);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string code)
        {
            DATA_MODELS.User returnUser = await userManager.FindByIdAsync(user.Id);

            return await userManager.ConfirmEmailAsync(returnUser, code);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            DATA_MODELS.User returnUser = new DATA_MODELS.User
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                IsApproved = user.IsApproved
            };

            return await userManager.CreateAsync(returnUser, password);
        }

        public async Task<IdentityResult> CreateAsync(User user)
        {
            DATA_MODELS.User returnUser = new DATA_MODELS.User
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name
            };

            return await userManager.CreateAsync(returnUser);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            DATA_MODELS.User returnUser = await userManager.FindByIdAsync(user.Id);

            return await userManager.GenerateEmailConfirmationTokenAsync(returnUser);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            DATA_MODELS.User returnUser = await userManager.FindByIdAsync(user.Id);

            return await userManager.GeneratePasswordResetTokenAsync(returnUser);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            DATA_MODELS.User returnUser = await userManager.FindByIdAsync(user.Id);

            return await userManager.GetLoginsAsync(returnUser);
        }

        public string GetUserName(ClaimsPrincipal user)
        => userManager.GetUserName(user);

        public async Task<bool> HasPasswordAsync(User user)
        {
            DATA_MODELS.User returnUser = await userManager.FindByIdAsync(user.Id);

            return await userManager.HasPasswordAsync(returnUser);
        }

        public async Task<bool> IsEmailConfirmedAsync(User user)
        {
            DATA_MODELS.User returnUser = await userManager.FindByIdAsync(user.Id);

            return await userManager.IsEmailConfirmedAsync(returnUser);
        }

        public async Task<IdentityResult> RemoveLoginAsync(User user, string loginProvider, string providerKey)
        {
            DATA_MODELS.User returnUser = await userManager.FindByIdAsync(user.Id);

            return await userManager.RemoveLoginAsync(returnUser, loginProvider, providerKey);
        }

        public async Task<IdentityResult> RemovePasswordAsync(User user)
        {
            DATA_MODELS.User returnUser = await userManager.FindByIdAsync(user.Id);

            return await userManager.RemovePasswordAsync(returnUser);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string code, string password)
        {
            DATA_MODELS.User returnUser = await userManager.FindByIdAsync(user.Id);

            return await userManager.ResetPasswordAsync(returnUser, code, password);
        }

        public async Task<IdentityResult> SetEmailAsync(User user, string email)
        {
            DATA_MODELS.User returnUser = await userManager.FindByIdAsync(user.Id);

            return await userManager.SetEmailAsync(returnUser, email);
        }

        async Task<User> IUserService.FindByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            var returnUser = new User
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                IsApproved = user.IsApproved,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp
            };

            return returnUser;
        }

        async Task<User> IUserService.FindByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var returnUser = new User
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                IsApproved = user.IsApproved,
                SecurityStamp = user.SecurityStamp
            };

            return returnUser;
        }

        async Task<User> IUserService.GetUserAsync(ClaimsPrincipal user)
        {
            var dataUser = await userManager.FindByIdAsync(user.GetUserId());

            var returnUser = new User
            {
                Id = dataUser.Id,
                UserName = dataUser.UserName,
                Name = dataUser.Name,
                Email = dataUser.Email,
                IsApproved = dataUser.IsApproved
            };

            return returnUser;
        }

        string IUserService.GetUserId(ClaimsPrincipal user)
        => userManager.GetUserId(user);
    }
}
