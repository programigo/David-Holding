using Microsoft.AspNetCore.Http;
using System.IO;

namespace TicketingSystem.Web.Infrastructure.Extensions
{
	public static class FormFileExtensions
	{
		public static byte[] ToByteArray(this IFormFile formFile)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				formFile.CopyTo(memoryStream);

				return memoryStream.ToArray();
			}
		}
	}
}
