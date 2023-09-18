namespace API.Profiles
{
    using AutoMapper;
    using Shared.Entities;
    using Shared.Models;

    public class ConnectionProfileMapper : Profile
    {
        public ConnectionProfileMapper()
        {
            CreateMap<ConnectionModel, Connection>();
            CreateMap<Connection, ConnectionModel>();
        }
    }
}
