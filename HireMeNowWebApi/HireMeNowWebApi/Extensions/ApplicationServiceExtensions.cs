

using HireMeNowWebApi.Data.Repositories;
using HireMeNowWebApi.Data.UnitOfWorks;
using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Repositories;
using HireMeNowWebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace HireMeNowWebApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
			services.AddDbContextPool<HireMeNowDbContext>(options =>
				options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
			);

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IInterviewServices, InterviewServices>();
			services.AddScoped<IInterviewRepository, InterviewRepository>();
			services.AddScoped<IJobService, JobService>();
			services.AddScoped<IJobRepository, JobRepository>();
			services.AddScoped<ICompanyRepository, CompanyRepository>();
			services.AddScoped<ICompanyService, CompanyService>();
			services.AddScoped<IApplicationService, ApplicationService>();
			services.AddScoped<IApplicationRepository, ApplicationRepository>();

			//services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
			//services.AddDbContextPool<HireMeNowDbContext>(options =>
			//	options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
			//);
			//services.AddScoped<HireMeNowDbContext>();
			return services;
		}
    }
}