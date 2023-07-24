using HireMeNowWebApi.Exceptions;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private List<Company> companies = new List<Company>();
        HireMeNowDbContext context;

		public CompanyRepository(HireMeNowDbContext _context, AutoMapper.IMapper _mapper)
        {
            context= _context;
        }
        public List<Company> getAllCompanies(string? name)
        {
            if(name == null)    
            return companies;
            else return companies.Where(e=>e.Name.Contains(name,StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public Company? getById(Guid id)
        {
           return companies.Where(c=>c.Id == id).FirstOrDefault();
        }

        public Company? Register(Company company)
        {
            company.Id=Guid.NewGuid();
            companies.Add(company);
            return company;
        }

        public Company Update(Company company)
        {
            int indexToUpdate = companies.FindIndex(item => item.Id == company.Id);
            if (indexToUpdate != -1)
            {
                // Modify the properties of the item at the found index
                companies[indexToUpdate].About = company.About ?? companies[indexToUpdate].About;
                companies[indexToUpdate].Name = company.Name ?? companies[indexToUpdate].Name;
                companies[indexToUpdate].Email = company.Email ?? companies[indexToUpdate].Email;
                companies[indexToUpdate].Website = company.Website ?? companies[indexToUpdate].Website;
                companies[indexToUpdate].Vision = company.Vision??companies[indexToUpdate].Vision;
                companies[indexToUpdate].Mission = company.Mission??companies[indexToUpdate].Mission;
                companies[indexToUpdate].Location = company.Location??companies[indexToUpdate].Location;
                companies[indexToUpdate].Address = company.Address??companies[indexToUpdate].Address;
                //companies[indexToUpdate].Logo = company.Address??companies[indexToUpdate].Address;
                companies[indexToUpdate].Phone = company.Phone==null ? companies[indexToUpdate].Phone : company.Phone;

            }
            else
            {
                throw new NotFoundException("Company Not Found");
            }

            return companies[indexToUpdate];
        }
    }
}
