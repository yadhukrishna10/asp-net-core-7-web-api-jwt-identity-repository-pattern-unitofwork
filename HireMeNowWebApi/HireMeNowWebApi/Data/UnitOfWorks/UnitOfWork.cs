using AutoMapper;
using HireMeNowWebApi.Data.Repositories;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Repositories;

namespace HireMeNowWebApi.Data.UnitOfWorks
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly HireMeNowDbContext _context;
		private readonly IMapper _mapper;
		public UnitOfWork(HireMeNowDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public IUserRepository UserRepository => new UserRepository(_context,_mapper);

		public ICompanyRepository CompanyRepository =>  new CompanyRepository(_context,_mapper);

		public IJobRepository JobRepository =>  new JobRepository(_context,_mapper);

		public IInterviewRepository InterviewRepository =>  new InterviewRepository(_context,_mapper);

		public IApplicationRepository ApplicationRepository =>  new ApplicationRepository(_context,_mapper);

		public async Task<bool> Complete()
		{
			return await _context.SaveChangesAsync() > 0;
		}

		public bool HasChanges()
		{
			_context.ChangeTracker.DetectChanges();
			var changes = _context.ChangeTracker.HasChanges();

			return changes;
		}
	}
}
