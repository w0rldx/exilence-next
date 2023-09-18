namespace API.Profiles
{
    using AutoMapper;
    using Shared.Entities;
    using Shared.Models;

    public class StashtabProfileMapper : Profile
    {
        public StashtabProfileMapper()
        {
            CreateMap<StashtabModel, StashTab>();
            CreateMap<StashTab, StashtabModel>()
                .ForMember(o => o.Id, opt => opt.Ignore());
        }
    }
}
