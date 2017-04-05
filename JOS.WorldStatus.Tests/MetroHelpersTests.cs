using System;
using JOS.WorldStatus.Features.Metro;
using Shouldly;
using Xunit;

namespace JOS.WorldStatus.Tests
{
	public class MetroHelpersTests
	{
		[Theory]
		[InlineData("10 min", 10)]
		[InlineData("9 min", 9)]
		[InlineData("8 min", 8)]
		[InlineData("7 min", 7)]
		[InlineData("6 min", 6)]
		[InlineData("5 min", 5)]
		[InlineData("4 min", 4)]
		[InlineData("3 min", 3)]
		[InlineData("2 min", 2)]
		[InlineData("1 min", 1)]
		public void GivenTimeRemainingText_WhenTimeTabled_ThenReturnsCorrectDateTime(string input, int minutes)
		{
			var result = MetroHelpers.TimeTabled(input);
			var expected = DateTime.UtcNow.AddMinutes(minutes);

			result.Value.Hour.ShouldBe(expected.Hour);
			result.Value.Minute.ShouldBe(expected.Minute);
		}

		[Theory]
		[InlineData("00:00", 00)]
		[InlineData("01:00", 01)]
		[InlineData("02:00", 02)]
		[InlineData("03:00", 03)]
		[InlineData("04:00", 04)]
		[InlineData("05:00", 05)]
		[InlineData("06:00", 06)]
		[InlineData("07:00", 07)]
		[InlineData("08:00", 08)]
		[InlineData("09:00", 09)]
		[InlineData("10:00", 10)]
		[InlineData("11:00", 11)]
		[InlineData("12:00", 12)]
		[InlineData("13:00", 13)]
		[InlineData("14:00", 14)]
		[InlineData("15:00", 15)]
		[InlineData("16:00", 16)]
		[InlineData("17:00", 17)]
		[InlineData("18:00", 18)]
		[InlineData("19:00", 19)]
		[InlineData("20:00", 20)]
		[InlineData("21:00", 21)]
		[InlineData("22:00", 22)]
		[InlineData("23:00", 23)]
		public void GivenTimeText_WhenTimeTabled_ThenReturnsCorrectDateTimeHour(string input, int hour)
		{
			var result = MetroHelpers.TimeTabled(input);

			result.Value.Hour.ShouldBe(hour);
		}

		[Theory]
		[InlineData("00:00", 00)]
		[InlineData("00:01", 01)]
		[InlineData("00:02", 02)]
		[InlineData("00:03", 03)]
		[InlineData("00:04", 04)]
		[InlineData("00:05", 05)]
		[InlineData("00:06", 06)]
		[InlineData("00:07", 07)]
		[InlineData("00:08", 08)]
		[InlineData("00:09", 09)]
		[InlineData("00:10", 10)]
		[InlineData("00:20", 20)]
		[InlineData("00:30", 30)]
		[InlineData("00:40", 40)]
		[InlineData("00:50", 50)]
		[InlineData("00:51", 51)]
		[InlineData("00:52", 52)]
		[InlineData("00:53", 53)]
		[InlineData("00:54", 54)]
		[InlineData("00:55", 55)]
		[InlineData("00:56", 56)]
		[InlineData("00:57", 57)]
		[InlineData("00:58", 58)]
		[InlineData("00:59", 59)]
		public void GivenTimeText_WhenTimeTabled_ThenReturnsCorrectDateTimeMinute(string input, int minute)
		{
			var result = MetroHelpers.TimeTabled(input);

			result.Value.Minute.ShouldBe(minute);
		}

		[Fact]
		public void GivenNuText_WhenTimeTabled_ThenReturnsCorrectDateTimeHour()
		{
			var now = DateTime.UtcNow;

			var result = MetroHelpers.TimeTabled("Nu");

			result.Value.Hour.ShouldBe(now.Hour);
		}

		[Fact]
		public void GivenNuText_WhenTimeTabled_ThenReturnsCorrectDateTimeMinute()
		{
			var now = DateTime.UtcNow;

			var result = MetroHelpers.TimeTabled("Nu");

			result.Value.Minute.ShouldBe(now.Minute);
		}

		[Fact]
		public void GivenNullText_WhenTimeTabled_ThenReturnsNull()
		{
			var result = MetroHelpers.TimeTabled(null);

			result.ShouldBeNull();
		}

		[Fact]
		public void GivenDashText_WhenTimeTabled_ThenReturnsNull()
		{
			var result = MetroHelpers.TimeTabled("-");

			result.ShouldBeNull();
		}
	}
}
