using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Interfaces
{
    public interface ICompanyService
    {
        User memberRegister(User user);
        public List<User> memberListing(Guid companyId);
        void memberDeleteById(Guid id);
        Company? Register(Company company);
        Company Update(Company company);
        List<Company> GetAllCompany(string? name=null);
        Company? getCompanyById(Guid value);

    }
}
