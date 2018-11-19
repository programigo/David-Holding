using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TicketingSystem.VueTS.Common.Constants;

namespace TicketingSystem.VueTS.Infrastructure.Extensions
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
