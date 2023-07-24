namespace HireMeNowWebApi.Helpers
{
	public class JobListParams: PaginationParams
	{
		public int JobType { get; set; } 
		public string? JobTitle { get; set; }
	}
}
