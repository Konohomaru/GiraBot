namespace GitHub
{
	public class Owner
	{
		public string Login { get; private set; }

		public Owner(string login)
		{
			Login = login;
		}

		internal static Owner BuildFrom(Octokit.Account account)
		{
			return new Owner(account.Login);
		}
	}
}
