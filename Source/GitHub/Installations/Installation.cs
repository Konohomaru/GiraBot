namespace GitHub
{
	public class Installation
	{
		public long Id { get; private set; }

		public Owner Owner { get; private set; }

		public Installation(long id, Owner owner)
		{
			Id = id;
			Owner = owner;
		}

		internal static Installation BuildFrom(Octokit.Installation installation, Owner owner)
		{
			return new Installation(installation.Id, owner);
		}
	}
}
