using AutoMapper;
using CSharpClicker.Web.Domain;
using CSharpClicker.Web.UseCases.GetArmors;
using CSharpClicker.Web.UseCases.GetBooses;
using CSharpClicker.Web.UseCases.GetBoosts;
using CSharpClicker.Web.UseCases.GetCurrentUser;
using CSharpClicker.Web.UseCases.GetWeapons;

namespace CSharpClicker.Web.UseCases;

public class MappingProfie : Profile
{
    public MappingProfie()
    {
        CreateMap<Boost, BoostDto>();
        CreateMap<Boss, BossDto>();
        CreateMap<Armor, ArmorDto>();
        CreateMap<Weapon, WeaponDto>();
        CreateMap<UserBoost, UserBoostDto>();
        CreateMap<UserArmors, UserArmorsDto>();
        CreateMap<UserWeapons, UserWeaponsDto>();
        CreateMap<UserBosses, UserBossesDto>();
        CreateMap<ApplicationUser, UserDto>();

       


        
    }
}
