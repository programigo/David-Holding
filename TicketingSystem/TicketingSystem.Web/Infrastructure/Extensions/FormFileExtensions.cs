﻿namespace TicketingSystem.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Http;
    using System.IO;

    public static class FormFileExtensions
    {
        public static byte[] ToByteArray(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);

                return memoryStream.ToArray();
            }
        }
    }
}
