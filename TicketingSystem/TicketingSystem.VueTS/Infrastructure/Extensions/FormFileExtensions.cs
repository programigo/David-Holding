using Microsoft.AspNetCore.Http;
using System.IO;

namespace TicketingSystem.VueTS.Infrastructure.Extensions
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
