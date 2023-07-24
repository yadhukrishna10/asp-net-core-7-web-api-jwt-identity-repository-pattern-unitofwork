using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Repositories;

namespace HireMeNowWebApi.Services
{
    public class CompanyService : ICompanyService
    {
        public IUserRepository _UserRepository;

        public ICompanyRepository _CompanyRepository;

        public CompanyService(IUserRepository userRepository, ICompanyRepository companyRepository)
        {
            _UserRepository = userRepository;
            _CompanyRepository = companyRepository;
        }

        public User memberRegister(User user)
        {
            return _UserRepository.memberRegister(user);
        }

        public List<User> memberListing(Guid companyId)
        {
            return _UserRepository.memberListing(companyId);
        }
        public void memberDeleteById(Guid id)
        {
            _UserRepository.memberDeleteById(id);
        }



        public Company? Register(Company company)
        {
            return _CompanyRepository.Register(company);
        }


        public List<Company> GetAllCompany(string? name=null)
        {
            return _CompanyRepository.getAllCompanies(name);
        }

        public Company? getCompanyById(Guid id)
        {
            return _CompanyRepository.getById(id);
        }

        public Company Update(Company company)
        {
            return _CompanyRepository.Update(company);
        }





    }
}
