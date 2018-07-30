using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Common.Constants;

namespace TicketingSystem.Web.Areas.Projects.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebConstants.AdministratorRole)]
    public abstract class BaseAdminController : Controller
    {
    }
}
