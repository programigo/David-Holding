namespace TicketingSystem.Services
{
	public class TokenOptions
	{
		public string EmailConfirmationTokenProvider { get; set; }

		public string PasswordResetTokenProvider { get; set; }

		public string ChangeEmailTokenProvider { get; set; }

		public string ChangePhoneNumberTokenProvider { get; set; }

		public string AuthenticatorTokenProvider { get; set; }
	}
}
