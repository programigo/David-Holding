namespace TicketingSystem.Services.Tickets.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;
    using System.Collections.Generic;

    public class TicketListingServiceModel : IMapFrom<Ticket>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public DateTime PostTime { get; set; }

        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public string Sender { get; set; }

        public string SenderId { get; set; }

        public TicketType TicketType { get; set; }

        public TicketState TicketState { get; set; }
      
        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] AttachedFiles { get; set; }

        public List<MessageListingServiceModel> Messages { get; set; } = new List<MessageListingServiceModel>();

        public void ConfigureMapping(Profile autoMapperProfile)
        => autoMapperProfile
            .CreateMap<Ticket, TicketListingServiceModel>()
            .ForMember(t => t.Sender, cfg => cfg.MapFrom(t => t.Sender.Name))
            .ForMember(t => t.Project, cfg => cfg.MapFrom(t => t.Project))
            .ForMember(t => t.AttachedFiles, cfg => cfg.MapFrom(t => t.AttachedFiles))
            .ForMember(t => t.Messages, cfg => cfg.MapFrom(t => t.Messages));
    }
}
