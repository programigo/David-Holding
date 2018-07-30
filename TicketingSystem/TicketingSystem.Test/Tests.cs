using AutoMapper;
using TicketingSystem.Web.Infrastructure.Mapping;

namespace TicketingSystem.Test
{
    public class Tests
    {
        private static bool testInitialized = false;


        public static void Initialize()
        {
            if (!testInitialized)
            {
                Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
                testInitialized = true;
            }
        }
    }
}
