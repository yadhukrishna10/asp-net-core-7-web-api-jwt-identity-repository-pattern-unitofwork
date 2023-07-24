using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Repositories;

namespace HireMeNowWebApi.Services
{
    public class JobService : IJobService
    {
        IJobRepository _jobRepository;
        public JobService(IJobRepository jobRepository )
        {
            _jobRepository=jobRepository;
        }

	
			
		public void DeleteItemById(Guid id)
		{
			_jobRepository.DeleteById(id);

		}

		public List<Job> getByTitle(string title)
		{
			
				return _jobRepository.getByTitle(title);
		

		}

		public Job getJobById(Guid selectedJobId)
		{
			return _jobRepository.GetJobById(selectedJobId);
		}

		public async Task<List<Job>> GetJobs()
        {
           return await _jobRepository.GetJobsAsync();
        }

		

		public void PostJob(Job job)
        {
			_jobRepository.Create(job);
        }

		public async Task<Job> Update(Job job)
		{
			var updatedjob =await  _jobRepository.UpdateAsync(job);
			return updatedjob;
		}
	}
}
