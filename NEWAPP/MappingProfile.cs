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


            CreateMap<Author, AuthorViewModel>();
            CreateMap<AuthorViewModel, Author>();

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


            CreateMap<CookingRecipe, CookingRecipeViewModel>();
            CreateMap<CookingRecipeViewModel, CookingRecipe>();

            CreateMap<Image, ImageViewModel>();
            CreateMap<ImageViewModel, Image>();

            CreateMap<PsplCustomer, PsplCustomerViewModel>();
            CreateMap<PsplCustomerViewModel, PsplCustomer>();


            CreateMap<ITInstitute, ITInstituteViewModel>()
            .ForMember(dest => dest.CreatedDateFormatted, opt => opt.MapFrom(src => src.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"))) // Format CreatedDate
            .ForMember(dest => dest.ModifiedDateFormatted, opt => opt.MapFrom(src => src.ModifiedDate.HasValue ? src.ModifiedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A")) // Format ModifiedDate
            .ForMember(dest => dest.FormattedRating, opt => opt.MapFrom(src => src.Rating.HasValue ? $"{src.Rating.Value:F1} / 5" : "No Rating"));

            CreateMap<ITInstituteViewModel, ITInstitute>()
     .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src =>
         !string.IsNullOrWhiteSpace(src.CreatedDateFormatted)
             ? NameSplitter.ParseDate(src.CreatedDateFormatted) // Custom parser for string to DateTime
             : DateTime.Now)) // Default value for null or empty input
     .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src =>
         !string.IsNullOrWhiteSpace(src.ModifiedDateFormatted)
             ? NameSplitter.ParseDate(src.ModifiedDateFormatted) // Custom parser for string to DateTime
             : (DateTime?)null)) // Null value for missing input
     .ForMember(dest => dest.Rating, opt => opt.MapFrom(src =>
         NameSplitter.ParseFormattedRating(src.FormattedRating))); // Parsing formatted rating


          


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
        public static decimal? ParseFormattedRating(string formattedRating)
        {
            if (string.IsNullOrWhiteSpace(formattedRating))
                return null;

            var parts = formattedRating.Split('/');
            return decimal.TryParse(parts[0], out var result) ? result : (decimal?)null;
        }

        public static DateTime ParseDate(string date)
        {
            var parsedDate = DateTime.Now;
            parsedDate = Convert.ToDateTime(date);
            return parsedDate;
        }

       
    }
   

}
