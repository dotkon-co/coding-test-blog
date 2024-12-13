using AutoMapper;
using CodingBlog.Presentation.Controllers.Mappers;

namespace CodingBlog.Presentation.Setups;

public static class SetupAutoMapper
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddSingleton<IMapper, Mapper>(_ =>
        {
            var mapper = new Mapper(new MapperConfiguration(SetupApiMapperProfiles));
            return mapper;
        });

        return services;
    }

    public static void SetupApiMapperProfiles(IMapperConfigurationExpression configuration)
    {
        configuration.AddProfile<PostMapper>();
    }
}