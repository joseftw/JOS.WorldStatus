namespace JOS.WorldStatus.Domain
{
	public class Deviation
	{
		public Deviation(
			int level, 
			string description, 
			string consequence
		)
		{
			Level = level;
			Description = description;
			Consequence = consequence;
		}

		public int Level { get; }
		public string Description { get; }
		public string Consequence { get; }
	}
}