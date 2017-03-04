namespace JOS.WorldStatus.Domain
{
	public class StopPointDeviation
	{
		public StopPointDeviation(
			string stopPointName,
			TransportMode transportMode,
			Deviation deviation
		)
		{
			StopPointName = stopPointName;
			TransportMode = transportMode;
			Deviation = deviation;
		}
		public string StopPointName { get; }
		public TransportMode TransportMode { get; }
		public Deviation Deviation { get; }
	}
}