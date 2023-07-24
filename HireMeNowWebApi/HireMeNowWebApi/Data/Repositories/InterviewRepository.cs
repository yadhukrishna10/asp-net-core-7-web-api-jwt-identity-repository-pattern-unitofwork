using AutoMapper;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HireMeNowWebApi.Repositories
{
	public class InterviewRepository : IInterviewRepository
	{
		private readonly IMapper _mapper;
		private HireMeNowDbContext _context;
        public InterviewRepository(HireMeNowDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		List<Interview> interviews = new List<Interview>();
		//{ new Interview(new Guid(), "TCS", "Developer", "10/02/2023", "Mumbai", "10.00"), new Interview(new Guid(), "Wipro", "Developer", "11/02/2023", "EKm", "12.00"), new Interview(new Guid(), "anglo", "Accountant", "24/02/2023", "Tcr", "12.00") };
		public Interview shduleInterview(Interview interview)
		{
			interview.Id = Guid.NewGuid();
			interviews.Add(interview);
			return interview;
			
		}
		public List<Interview> sheduledInterviewList()
		{
			List<Interview> listofinterviews = interviews.ToList();
			return listofinterviews;

		}
		public void removeInterview(Guid id)
		{
			Interview interview = interviews.FirstOrDefault(e => e.Id == id);
			if(interview!=null)
			{
				interviews.Remove(interview);
			}
			
		}

	}
}
