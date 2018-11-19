using AutoMapper;

namespace TicketingSystem.VueTS.Infrastructure.Mapping
{
	public interface IHaveCustomMapping
	{
		void ConfigureMapping(Profile mapper);
	}
}
