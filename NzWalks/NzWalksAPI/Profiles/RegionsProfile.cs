using AutoMapper;

namespace NzWalksAPI.Profiles
{
    public class RegionsProfile: Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>().ReverseMap();
            
                
                //the code below is added when the two models have different variable names
                //.ForMember(dest => dest.Id, options => options.MapFrom(src => src.RegionId));
        }
    }
}
