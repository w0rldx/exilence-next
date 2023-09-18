namespace API.Profiles
{
    using AutoMapper;
    using Shared.Entities;
    using Shared.Models;

    public class AccountProfileMapper : Profile
    {
        public AccountProfileMapper()
        {
            CreateMap<AccountModel, Account>()
                .ForMember(o => o.Id, opt => opt.Ignore());
            CreateMap<Account, AccountModel>();
        }
    }
}
