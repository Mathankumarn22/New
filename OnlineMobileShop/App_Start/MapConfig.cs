using OnlineMobileShop.Models;
using OnlineMobileShop.Entity;
using FluentNHibernate.Automapping;

namespace OnlineMobileShop.App_Start
{
    public static class MapConfig
    {
        public static void Map()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Models.SignUp, Account>();
                config.CreateMap<Models.LogIn, Account>();
                config.CreateMap<Models.AddMobiles, Mobile>();
                config.CreateMap<Mobile, Models.AddMobiles>();
            });

        }
    }
}