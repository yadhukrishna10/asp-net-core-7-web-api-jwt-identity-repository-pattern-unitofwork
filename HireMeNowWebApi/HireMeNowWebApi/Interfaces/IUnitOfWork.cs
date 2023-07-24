using HireMeNowWebApi.Interfaces;

namespace HireMeNowWebApi.Data.UnitOfWorks
{
	public interface IUnitOfWork
	{
		IUserRepository UserRepository { get; }
		ICompanyRepository CompanyRepository { get; }
		IJobRepository JobRepository { get; }
		IInterviewRepository InterviewRepository { get; }
		IApplicationRepository ApplicationRepository { get; }

		Task<bool> Complete();
		bool HasChanges();
	}
}
