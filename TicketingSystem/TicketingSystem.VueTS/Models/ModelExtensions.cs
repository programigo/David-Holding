using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Text;

namespace TicketingSystem.VueTS.Models
{
    internal static class ModelExtensions
    {
        public static InternalErrorModel ToInternalErrorModel(this Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var model = new InternalErrorModel
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace,
                InnerError = exception.InnerException?.ToInternalErrorModel(),
            };

            return model;
        }

        public static BadRequestErrorModel ToBadRequestErrorModel(this ModelStateDictionary modelStateEntries)
        {
            if (modelStateEntries == null)
            {
                throw new ArgumentNullException(nameof(modelStateEntries));
            }

            var sb = new StringBuilder();

            foreach (ModelStateEntry entry in modelStateEntries.Values)
            {
                foreach (ModelError error in entry.Errors)
                {
                    sb.AppendFormat("{0} ", error.ErrorMessage);
                }
            }

            var model = new BadRequestErrorModel
            {
                Type = BadRequestErrorType.ModelState,
                Message = sb.ToString().Trim(),
            };

            return model;
        }

        public static UnauthorizedErrorModel ToUnauthorizedErrorModel(this ModelStateDictionary modelStateEntries)
        {
            if (modelStateEntries == null)
            {
                throw new ArgumentNullException(nameof(modelStateEntries));
            }

            var sb = new StringBuilder();

            foreach (ModelStateEntry entry in modelStateEntries.Values)
            {
                foreach (ModelError error in entry.Errors)
                {
                    sb.AppendFormat("{0} ", error.ErrorMessage);
                }
            }

            var model = new UnauthorizedErrorModel
            {
                Type = UnauthorizedErrorType.NotApproved,
                Message = sb.ToString().Trim(),
            };

            return model;
        }
    }
}
