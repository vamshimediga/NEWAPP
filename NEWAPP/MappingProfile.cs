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
        }
    }
}
