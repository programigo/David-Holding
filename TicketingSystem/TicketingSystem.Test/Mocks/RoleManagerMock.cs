using Microsoft.AspNetCore.Identity;
using Moq;

namespace TicketingSystem.Test.Mocks
{
    public class RoleManagerMock
    {
        public static Mock<RoleManager<IdentityRole>> New
            => new Mock<RoleManager<IdentityRole>>(Mock.Of<IUserStore<IdentityRole>>(), null, null, null, null, null, null, null, null);
    }
}