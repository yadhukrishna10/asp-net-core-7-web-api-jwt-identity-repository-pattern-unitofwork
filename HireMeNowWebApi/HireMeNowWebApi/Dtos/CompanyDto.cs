namespace HireMeNowWebApi.Dtos
{
    public class CompanyDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }
        public string? About { get; set; }
        public string? Vision { get; set; }
        public string? Mission { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }

        public CompanyDto()
        {
            
        }
        public CompanyDto(string? name, string? email, string? website, string? phone, string? image, string? about, string? vision, string? mission, string? location, string? address, Guid? id = null)
        {
            Id=id??new Guid();
            Name=name;
            Email=email;
            Website=website;
            Phone=phone;
            Image=image;
            About=about;
            Vision=vision;
            Mission=mission;
            Location=location;
            Address=address;
        }
    }
}
