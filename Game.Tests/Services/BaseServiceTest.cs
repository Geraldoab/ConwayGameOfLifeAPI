using AutoMapper;
using Game.Application.Mapping.Profiles;

namespace Game.Tests.Services
{
    public abstract class BaseServiceTest
    {
        protected virtual IMapper CreateIMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BoardProfile());
            });
            return  mappingConfig.CreateMapper();
        }
    }
}
