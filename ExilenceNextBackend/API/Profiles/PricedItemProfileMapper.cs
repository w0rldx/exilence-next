namespace API.Profiles
{
    using AutoMapper;
    using Shared.Entities;
    using Shared.Models;

    public class PricedItemProfileMapper : Profile
    {
        public PricedItemProfileMapper()
        {
            CreateMap<PricedItemModel, PricedItem>();
            CreateMap<PricedItem, PricedItemModel>()
                .ForMember(o => o.Id, opt => opt.Ignore());
        }
    }
}
