namespace API.Profiles
{
    using AutoMapper;
    using Shared.Entities;
    using Shared.Models;

    public class SnapshotProfileMapper : Profile
    {
        public SnapshotProfileMapper()
        {
            CreateMap<SnapshotModel, Snapshot>();
            CreateMap<Snapshot, SnapshotModel>()
                .ForMember(o => o.Id, opt => opt.Ignore());
        }
    }
}
