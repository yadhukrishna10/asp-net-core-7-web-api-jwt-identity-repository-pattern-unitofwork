using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Interfaces
{
	public interface IApplicationRepository
	{
		public List<Application> GetAllByCompany(Guid comapnyId);
		public void AddApplication(Application application);
	}
}
