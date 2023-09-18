namespace API.Profiles
{
    using AutoMapper;
    using Shared.Entities;
    using Shared.Models;

    public class SnapshotProfileProfileMapper : Profile
    {
        public SnapshotProfileProfileMapper()
        {
            CreateMap<SnapshotProfileModel, SnapshotProfile>()
                .ForMember(o => o.Id, opt => opt.Ignore());
            CreateMap<SnapshotProfile, SnapshotProfileModel>();
        }
    }
}
