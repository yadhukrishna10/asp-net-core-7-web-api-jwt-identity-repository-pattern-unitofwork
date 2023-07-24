namespace HireMeNowWebApi.Dtos
{
	public class InterviewDto
	{
		public Guid Id { get; set; }
		public Guid JobId { get; set; }
		public string Company { get; set; }
		public string Post { get; set; }
		public string Date { get; set; }
		public string Location { get; set; }
		public string Time { get; set; }

		public InterviewDto(Guid jobId,string company, string post, string date, string location, string time)
		{
			Id = Guid.NewGuid();
			  JobId = jobId;
			Company = company;
			Post = post;
			Date = date;
			Location = location;
			Time = time;
		}
		public InterviewDto()
		{

		}
	}
}
