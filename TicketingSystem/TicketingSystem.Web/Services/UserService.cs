using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketingSystem.Services;
using TicketingSystem.Web.Infrastructure.Extensions;
using DATA_MODELS = TicketingSystem.Data.Models;
//using ExternalLoginInfo = Microsoft.AspNetCore.Identity.ExternalLoginInfo;
using IdentityError = TicketingSystem.Services.IdentityError;
using ExternalLoginInfo = TicketingSystem.Services.ExternalLoginInfo;
using IdentityResult = TicketingSystem.Services.IdentityResult;
using UserLoginInfo = TicketingSystem.Services.UserLoginInfo;
using System.Linq;
using DATA = TicketingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace TicketingSystem.Web.Services
{
    public class UserService : UserManager<DATA_MODELS.User>, IUserService
    {
        private readonly DATA.TicketingSystemDbContext db;

        public UserService(DATA.TicketingSystemDbContext db, IUserStore<DATA_MODELS.User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<DATA_MODELS.User> passwordHasher, IEnumerable<IUserValidator<DATA_MODELS.User>> userValidators, IEnumerable<IPasswordValidator<DATA_MODELS.User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<DATA_MODELS.User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.db = db;
        }

        IdentityOptionsModel IUserService.Options { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

            var newInfo = new Microsoft.AspNetCore.Identity.ExternalLoginInfo(info.Principal, info.LoginProvider, info.ProviderKey, info.ProviderDisplayName);

            var res = await base.AddLoginAsync(returnUser, newInfo);

            return Result(res);
        }

        public async Task<IdentityResult> AddPasswordAsync(User user, string password)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            var res = await base.AddPasswordAsync(returnUser, password);

            return Result(res);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            var res = await base.AddToRoleAsync(returnUser, role);

            return Result(res);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            var res = await base.ChangePasswordAsync(returnUser, oldPassword, newPassword);

            return Result(res);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string code)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            var res = await base.ConfirmEmailAsync(returnUser, code);

            return Result(res);
        }

        public async Task<int> CountRecoveryCodesAsync(User user)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            return await base.CountRecoveryCodesAsync(returnUser);
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

            var res = await base.CreateAsync(returnUser, password);

            return Result(res);
        }

        public async Task<IdentityResult> CreateAsync(User user)
        {
            DATA_MODELS.User returnUser = new DATA_MODELS.User
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name
            };

            var res = await base.CreateAsync(returnUser);

            return Result(res);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            return await base.GenerateEmailConfirmationTokenAsync(returnUser);
        }

        public async Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(User user, int number)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            return await base.GenerateNewTwoFactorRecoveryCodesAsync(returnUser, number);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            return await base.GeneratePasswordResetTokenAsync(returnUser);
        }

        public async Task<string> GetAuthenticatorKeyAsync(User user)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            return await base.GetAuthenticatorKeyAsync(returnUser);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            var res = await base.GetLoginsAsync(returnUser);

            var returnRes = new List<UserLoginInfo>();

            foreach (var loginInfo in res)
            {
                var systemLoginInfo = new UserLoginInfo(loginInfo.LoginProvider, loginInfo.ProviderKey, loginInfo.ProviderDisplayName);

                returnRes.Add(systemLoginInfo);
            }

            return returnRes;
        }

        public async Task<bool> HasPasswordAsync(User user)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            return await base.HasPasswordAsync(returnUser);
        }

        public async Task<bool> IsEmailConfirmedAsync(User user)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            return await base.IsEmailConfirmedAsync(returnUser);
        }

        public async Task<IdentityResult> RemoveLoginAsync(User user, string loginProvider, string providerKey)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            var res = await base.RemoveLoginAsync(returnUser, loginProvider, providerKey);

            return Result(res);
        }

        public async Task<IdentityResult> RemovePasswordAsync(User user)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            var res = await base.RemovePasswordAsync(returnUser);

            return Result(res);
        }

        public async Task<IdentityResult> ResetAuthenticatorKeyAsync(User user)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            var res = await base.ResetAuthenticatorKeyAsync(returnUser);

            return Result(res);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string code, string password)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            var res = await base.ResetPasswordAsync(returnUser, code, password);

            return Result(res);
        }

        public async Task<IdentityResult> SetEmailAsync(User user, string email)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            var res = await base.SetEmailAsync(returnUser, email);

            return Result(res);
        }

        public async Task<IdentityResult> SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            var res = await base.SetTwoFactorEnabledAsync(returnUser, enabled);

            return Result(res);
        }

        public async Task<bool> VerifyTwoFactorTokenAsync(User user, string authenticatorTokenProvider, string verificationCode)
        {
            DATA_MODELS.User returnUser = await base.FindByIdAsync(user.Id);

            return await base.VerifyTwoFactorTokenAsync(returnUser, authenticatorTokenProvider, verificationCode);
        }

        async Task<User> IUserService.FindByEmailAsync(string email)
        {
            var user = await base.FindByEmailAsync(email);

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
            var user = await base.FindByIdAsync(userId);

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
            var dataUser = await base.FindByIdAsync(user.GetUserId());

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
        => base.GetUserId(user);

        private static IdentityResult Result(Microsoft.AspNetCore.Identity.IdentityResult res)
        {
            var returnRes = new IdentityResult
            {
                Succeeded = res.Succeeded,
                //Errors = new List<IdentityError>(res.Errors),
            };


            return returnRes;
        }

        public async Task<Microsoft.AspNetCore.Identity.IdentityRole> GetUserRole(string id)
        {
            var roleId = await this.db.UserRoles.Where(r => r.UserId == id).FirstOrDefaultAsync();

            var role = await this.db.Roles.Where(r => r.Id == roleId.RoleId).FirstOrDefaultAsync();

            return role;
        }
    }
}
