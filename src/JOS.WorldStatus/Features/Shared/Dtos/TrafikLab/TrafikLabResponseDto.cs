namespace JOS.WorldStatus.Features.Shared.Dtos.TrafikLab
{
	public class TrafikLabResponseDto<T>
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public long ExecutionTime { get; set; }
		public T ResponseData { get; set; }
	}
}