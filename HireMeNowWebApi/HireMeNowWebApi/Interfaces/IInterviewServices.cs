using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Interfaces
{
	public interface IInterviewServices
	{
		Interview sheduleinterview(Interview interview);
		List<Interview> sheduledInterviewList();
		void removeInterview(Guid id);

	}
}
