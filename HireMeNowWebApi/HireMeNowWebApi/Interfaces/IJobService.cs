using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Interfaces
{
    public interface IJobService
    {
        public void PostJob(Job job);
        public Task<List<Job>> GetJobs();
        public Job getJobById(Guid selectedJobId);
		void DeleteItemById(Guid id);
		Task<Job> Update(Job job);
		List<Job> getByTitle(string title);
		
	}
}
