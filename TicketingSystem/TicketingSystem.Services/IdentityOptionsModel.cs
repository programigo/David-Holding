using Microsoft.AspNetCore.Identity;

namespace TicketingSystem.Services
{
    public class IdentityOptionsModel : IdentityOptions
    {
        private readonly IdentityOptions options;

        public IdentityOptionsModel(IdentityOptions options)
        {
            this.options = options;
        }
    }
}
