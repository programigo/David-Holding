namespace TicketingSystem.Services.Tickets.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class MessageListingServiceModel : IMapFrom<Message>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public DateTime PostDate { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public MessageState State { get; set; }

        public string Content { get; set; }

        public byte[] AttachedFiles { get; set; }

        public void ConfigureMapping(Profile autoMapperProfile)
        => autoMapperProfile
            .CreateMap<Message, MessageListingServiceModel>()
            .ForMember(m => m.Author, cfg => cfg.MapFrom(m => m.Author.Name))
            .ForMember(m => m.Ticket, cfg => cfg.MapFrom(m => m.Ticket))
            .ForMember(m => m.AttachedFiles, cfg => cfg.MapFrom(m => m.AttachedFiles));
    }
}
