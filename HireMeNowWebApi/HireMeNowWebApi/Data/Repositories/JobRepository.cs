using AutoMapper;
using AutoMapper.QueryableExtensions;
using HireMeNowWebApi.Enums;
using HireMeNowWebApi.Exceptions;
using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace HireMeNowWebApi.Data.Repositories
{
    public class JobRepository : IJobRepository
    {
        private List<Job> jobs = new();
        //	List<Job> { new Job("Dotnet Developer","Senior dotnet developer .","kochi","Fulltime","100000-300000",new Guid(),"Aitrich",new Guid("62ec44fb-9f30-4f45-8e3d-f3751998af89")),
        //new Job("Java Developer","Senior dotnet developer .","kochi","Fulltime","100000-300000",new Guid(),"Aitrich"),
        //new Job("Angular Developer","Senior dotnet developer .","kochi","Fulltime","100000-300000",new Guid(),"Aitrich"),
        //new Job("Dotnet Developer","Senior dotnet developer .","kochi","Fulltime","100000-300000",new Guid(),"Aitrich"),
        //new Job("Dotnet Developer","Senior dotnet developer .","kochi","Fulltime","100000-300000",new Guid(),"Aitrich")};
        private readonly List<Job> _jobs;
        HireMeNowDbContext _context;
        IMapper _mapper;

		public JobRepository(HireMeNowDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
        }

        public void DeleteById(Guid id)
        {
            Job item = jobs.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                jobs.Remove(item);
            }
        }

        public List<Job> getByTitle(string title)
        {
            //List<Job> job= (List<Job>)jobs.Where(e => e.Title == title);
            //return job;
            return jobs.Where(j => j.Title.ToLower().Contains(title.ToLower())).ToList();


        }

        public Job GetJobById(Guid selectedJobId)
        {
            return _context.Jobs.Find( selectedJobId);
        }

        public async Task<List<Job>> GetJobsAsync()
        {
            return await _context.Jobs.Include(a => a.Company).ToListAsync(); 
			
		}
        public async Task<PagedList<Job>> GetAllByFilter(JobListParams jobListParams)
        {
			var query = _context.Jobs
			   .OrderByDescending(c => c.CreatedDate)
			   //.ProjectTo<Job>(_mapper.ConfigurationProvider)
			   .AsQueryable();
		
			if (jobListParams.JobType > 0)
			{
				query = query.Where(c => c.JobType.Equals((Roles)jobListParams.JobType));
			}
			if (jobListParams.JobTitle !=null)
			{
				query = query.Where(c => c.Title.Contains(jobListParams.JobTitle));
			}


			return await PagedList<Job>.CreateAsync(query,
				jobListParams.PageNumber, jobListParams.PageSize);
		}

        public List<Job> GetJobsByIds(List<Guid> appliedJobsIds)
        {
            List<Job> Appliedjobs = new List<Job>();
            appliedJobsIds.ForEach(e => Appliedjobs.Add(GetJobById(e)));
            return Appliedjobs;


        }



        public async void Create(Job job)
        {
			 await _context.Jobs.AddAsync(job);
            _context.SaveChanges();
        }

        public async Task<Job> UpdateAsync(Job Updatedjob)
        {
            var jobToUpdate = _context.Jobs.Find(Updatedjob.Id);
            if (jobToUpdate != null)
            {
                // Modify the properties of the item at the found index
                jobToUpdate.Title = Updatedjob.Title ?? jobToUpdate.Title;
                jobToUpdate.Description = Updatedjob.Description ?? jobToUpdate.Description;
                jobToUpdate.Location = Updatedjob.Location ?? jobToUpdate.Location;
                jobToUpdate.Salary = Updatedjob.Salary ?? jobToUpdate.Salary;
                jobToUpdate.TypeOfWorkPlace = Updatedjob.TypeOfWorkPlace ?? jobToUpdate.TypeOfWorkPlace;
                jobToUpdate.Experience = Updatedjob.Experience ?? jobToUpdate.Experience;
                //jobToUpdate.CompanyName = Updatedjob.CompanyName ?? jobToUpdate.CompanyName;
                _context.Jobs.Update(jobToUpdate);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("Job Not Found");
            }
            return jobToUpdate;
        }
    }
}
