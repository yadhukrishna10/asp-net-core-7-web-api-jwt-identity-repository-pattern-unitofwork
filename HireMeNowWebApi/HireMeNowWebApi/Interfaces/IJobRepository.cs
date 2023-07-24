using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Interfaces
{
    public interface IJobRepository
    {
		void Create(Job job);
		Task<Job> UpdateAsync(Job job);
		Task<List<Job>> GetJobsAsync();
		Job GetJobById(Guid selectedJobId);
		List<Job> GetJobsByIds(List<Guid> appliedJobsIds);

		Task<PagedList<Job>> GetAllByFilter(JobListParams jobListParams);
		void DeleteById(Guid id);
		List<Job> getByTitle(string title);
	}
}
