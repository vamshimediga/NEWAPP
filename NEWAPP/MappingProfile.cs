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



        }
    }
}
