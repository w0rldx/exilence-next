namespace API.Profiles
{
    using AutoMapper;
    using Shared.Entities;
    using Shared.Models;

    public class LeagueProfileMapper : Profile
    {
        public LeagueProfileMapper()
        {
            CreateMap<LeagueModel, League>();
            CreateMap<League, LeagueModel>();
        }
    }
}
