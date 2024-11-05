using AutoMapper;
using DomainModels;
using NEWAPP.Models;

namespace NEWAPP
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<LeadSource, LeadSourceViewModel>()
            .ForMember(dest => dest.LeadSourceName, opt => opt.MapFrom(src => src.SourceName))
            .ForMember(dest => dest.CreatedDateFormatted, opt => opt.MapFrom(src => src.CreatedDate.ToString("MM/dd/yyyy"))); // Formatting date

            CreateMap<LeadSourceViewModel, LeadSource>()
            .ForMember(dest => dest.SourceName, opt => opt.MapFrom(src => src.LeadSourceName))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Parse(src.CreatedDateFormatted)));


            CreateMap<HumanResources, HumanResourcesViewModel>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate.ToString("yyyy-MM-dd")));

            CreateMap<Manager, ManagerViewModel>()
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<Country, CountriesViewModel>();
            //.ForMember(dest => dest.FormattedName, opt => opt.MapFrom(src => $"{src.Name} {src.Capital}"));

            CreateMap<Lead, LeadViewModel>();
            CreateMap<LeadViewModel, Lead>();

            CreateMap<UserData, UserDataViewModel>();
            CreateMap<UserDataViewModel, UserData>();

            CreateMap<Stock, StockViewModel>();

            CreateMap<PersonData, PersonDataViewModel>();
            CreateMap<PersonDataViewModel, PersonData>();

            CreateMap<AddressData, AddressDataViewModel>();
            CreateMap<AddressDataViewModel, AddressData>();

            CreateMap<Company, CompanyViewModel>();
            CreateMap<CompanyViewModel, Company>();

            CreateMap<Contect, ContectViewModel>();
            CreateMap<ContectViewModel, Contect>();

            CreateMap<online_retailUserLogin, online_retailUserLoginClientViewModel>();
            CreateMap<online_retailUserLoginClientViewModel, online_retailUserLogin>();

            CreateMap<AccessTable, AccessTableViewModel>();
            CreateMap<AccessTableViewModel, AccessTable>();

            CreateMap<UserLogin, UserLoginViewModel>()
           .ForMember(dest => dest.full_name, opt => opt.MapFrom(src => $"{src.first_name} {src.last_name}"));
            CreateMap<UserLoginViewModel, UserLogin>()
            .ForMember(dest => dest.first_name, opt => opt.MapFrom(src => NameSplitter.SplitFullName(src.full_name).firstName))
             .ForMember(dest => dest.last_name, opt => opt.MapFrom(src => NameSplitter.SplitFullName(src.full_name).lastName));




        }
    }
    public static class NameSplitter
    {
        public static (string firstName, string lastName) SplitFullName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return (string.Empty, string.Empty);

            var parts = fullName.Split(' ');
            var firstName = parts[0];
            var lastName = parts.Length > 1 ? parts[1] : string.Empty;

            return (firstName, lastName);
        }
    }
}
