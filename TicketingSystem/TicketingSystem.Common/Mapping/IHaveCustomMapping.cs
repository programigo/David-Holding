namespace TicketingSystem.Common.Mapping
{
    using AutoMapper;

    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile autoMapperProfile);
    }
}
