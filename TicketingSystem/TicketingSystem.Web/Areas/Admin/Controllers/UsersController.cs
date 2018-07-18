namespace TicketingSystem.Web.Areas.Projects.Controllers
{
    using Admin.Models.Users;
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using TicketingSystem.Web.Models.AccountViewModels;
    using Web.Areas.Projects.Models.Users;


    public class UsersController : BaseAdminController
    {
        private readonly IAdminUserService users;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(IAdminUserService users, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.users = users;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = this.users.All().Where(u => u.IsApproved == true && u.Username != WebConstants.AdministratorRole);
            var roles = this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            return View(new UserListingViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        public IActionResult Pending()
        {
            var users = this.users.All().Where(u => u.IsApproved == false && u.Username != WebConstants.AdministratorRole);

            return View(new UserPendingViewModel
            {
                Users = users
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleFormModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            var isAlreadyInRole = this.users.IsAlreadyInRole(user.Id);

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!isAlreadyInRole)
            {
                await this.userManager.AddToRoleAsync(user, model.Role);

                TempData.AddSuccessMessage($"User {user.UserName} successfully added to {model.Role} role.");

                return RedirectToAction(nameof(Index));
            }
            
            await this.userManager.AddToRoleAsync(user, model.Role);

            TempData.AddSuccessMessage($"User {user.UserName} successfully added to {model.Role} role.");
            
            
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Approve(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            this.users.Approve(id);

            TempData.AddSuccessMessage($"User {user.UserName} successfully approved.");
            return RedirectToAction(nameof(Pending));
        }

        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Name = model.Name,
                    IsApproved = true
                };
                await userManager.CreateAsync(user, model.Password);

                TempData.AddSuccessMessage($"User {user.UserName} successfully created");
                return RedirectToAction(nameof(Index));
            }
           
            return View(model);
        }
        
        public IActionResult Remove(string id)
        {
            this.users.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ChangeUserData(string id)
        {
            var user = this.users.Details(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult ChangeUserData(string id, AdminChangeDataViewModel model)
        {
            var changedUser = this.users.ChangeData(id, model.Name, model.Email);

            if (!changedUser)
            {
                return NotFound();
            }

            TempData.AddSuccessMessage($"User data for {model.Username} changed successfuly.");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangeUserPassword(string id)
        {
            var user = this.users.GetUser(id);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            var hasPassword = await userManager.HasPasswordAsync(user);

            var model = new AdminUserChangePasswordViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeUserPassword(string id, AdminUserChangePasswordViewModel model)
        {
            var user = this.users.GetUser(id);

            var result = await userManager.RemovePasswordAsync(user);

            if (result.Succeeded)
            {
                result = await userManager.AddPasswordAsync(user, model.NewPassword);
            }

            TempData.AddSuccessMessage($"Password for {user.UserName} changed successfuly.");

            return RedirectToAction(nameof(Index));
        }

        public string StatusMessage { get; private set; }
    }
}