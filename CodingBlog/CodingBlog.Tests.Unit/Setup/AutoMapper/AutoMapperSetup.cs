namespace CodingBlog.Tests.Unit.Setup.AutoMapper;

using global::AutoMapper;
using CodingBlog.Presentation.Setups;

public static class AutoMapperSetup
{
    public static void SetupApiMapperProfiles(IMapperConfigurationExpression configuration)
    {
        SetupAutoMapper.SetupApiMapperProfiles(configuration);
    }
}