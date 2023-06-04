using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Application.Interfaces;

namespace Comfy.Tests.Mock;

public static class MapperMock
{
    public static IMapper GetMapper()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
        });
        return mappingConfig.CreateMapper();
    }
}