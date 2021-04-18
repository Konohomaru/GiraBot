namespace Model
{
	public class Installation
	{
		public long Id { get; private set; }

		public string OwnerLogin { get; private set; }

		public Installation(long id, string owner)
		{
			Id = id;
			OwnerLogin = owner;
		}

		public static Installation BuildFrom(Octokit.Installation installation, string owner)
		{
			return new Installation(installation.Id, owner);
		}
	}
}
