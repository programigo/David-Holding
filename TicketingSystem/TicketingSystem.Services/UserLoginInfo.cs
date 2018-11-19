namespace TicketingSystem.Services
{
	public class UserLoginInfo
	{
		public UserLoginInfo(string loginProvider, string providerKey, string displayName)
		{
			this.LoginProvider = loginProvider;
			this.ProviderKey = providerKey;
			this.ProviderDisplayName = displayName;
		}

		public string LoginProvider { get; set; }

		public string ProviderKey { get; set; }

		public string ProviderDisplayName { get; set; }
	}
}
