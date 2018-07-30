using AutoMapper;

namespace TicketingSystem.Common.Mapping
{
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile autoMapperProfile);
    }
}
