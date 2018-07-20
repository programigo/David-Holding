namespace TicketingSystem.Test.Mocks
{
    using Microsoft.AspNetCore.Identity;
    using Moq;

    public class RoleManagerMock
    {
        public static Mock<RoleManager<IdentityRole>> New
            => new Mock<RoleManager<IdentityRole>>(Mock.Of<IUserStore<IdentityRole>>(), null, null, null, null, null, null, null, null);
    }
}
