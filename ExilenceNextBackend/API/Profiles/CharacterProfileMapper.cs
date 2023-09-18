namespace API.Profiles
{
    using AutoMapper;
    using Shared.Entities;
    using Shared.Models;

    public class CharacterProfileMapper : Profile
    {
        public CharacterProfileMapper()
        {
            CreateMap<CharacterModel, Character>();
            CreateMap<Character, CharacterModel>();
        }
    }
}
