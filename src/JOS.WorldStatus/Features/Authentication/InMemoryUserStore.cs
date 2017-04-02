using System.Collections.Generic;
using JOS.WorldStatus.Features.Shared;

namespace JOS.WorldStatus.Features.Authentication
{
	public class InMemoryUserStore : IUserStore
	{
		private static readonly Dictionary<string, string> Users = new Dictionary<string, string>
		{
			{
				"josef", "secret"
			}
		};

		public bool IsValid(string username, string password)
		{
			if (Users.ContainsKey(username))
			{
				var userPassword = Users[username];
				return userPassword == password;
			}

			return false;
		}
	}
}
