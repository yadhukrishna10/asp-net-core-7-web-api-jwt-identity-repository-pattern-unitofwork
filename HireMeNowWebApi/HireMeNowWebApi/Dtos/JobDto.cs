using HireMeNowWebApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HireMeNowWebApi.Dtos
{
	public class JobDto
	{
		public Guid Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? Location { get; set; }
		public string? Experience { get; set; }
		public string? TypeOfWork { get; set; }
		public string? Salary { get; set; }
	
		public string? CompanyName { get; set; }
		public Guid CompanyId { get; set; }
		
		//[JsonIgnore]
		//public Company? Company { get; set; }

		public JobDto(string title, string description, string location, string salary, Guid companyId, string company)
		{
			Id = Guid.NewGuid();
			Title = title;
			Description = description;
			Location = location;
			Salary = salary;
		
			CompanyName = company;
			CompanyId = companyId;
		}

		public JobDto()
		{
		}
	}
}

