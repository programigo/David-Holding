using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TicketingSystem.Common.Constants;

namespace TicketingSystem.Web.Infrastructure.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[WebConstants.TempDataSuccessMessageKey] = message;
        }

        public static void AddErrorMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[WebConstants.TempDataErrorMessageKey] = message;
        }
    }
}
