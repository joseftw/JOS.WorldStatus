using Microsoft.Extensions.Logging;

namespace JOS.WorldStatus
{
	public class LogEvents
	{
		public static EventId ProgramHostBeforeRun = new EventId(1000);
		public static EventId ProgramHostAfterRun = new EventId(1010);
	}
}