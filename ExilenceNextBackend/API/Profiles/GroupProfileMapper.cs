namespace API.Profiles
{
    using AutoMapper;
    using Shared.Entities;
    using Shared.Models;

    public class GroupProfileMapper : Profile
    {
        public GroupProfileMapper()
        {
            CreateMap<GroupModel, Group>();
            CreateMap<Group, GroupModel>();
        }
    }
}
