using AutoMapper;

namespace TicketingSystem.Web.Infrastructure.Mapping
{
	public interface IHaveCustomMapping
	{
		void ConfigureMapping(Profile mapper);
	}
}
