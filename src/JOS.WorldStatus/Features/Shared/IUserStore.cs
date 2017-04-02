namespace JOS.WorldStatus.Features.Shared
{
	public interface IUserStore
	{
		bool IsValid(string username, string password);
	}
}