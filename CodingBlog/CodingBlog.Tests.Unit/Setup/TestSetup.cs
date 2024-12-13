namespace CodingBlog.Tests.Unit.Setup;

using AutoFixture;
using AutoMapper;
using global::AutoMapper;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

public class TestSetup
{
    public readonly Fixture Fixture;
    public readonly IMapper Mapper;
    
    public TestSetup()
    {
        Fixture = new Fixture();
        Fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => Fixture.Behaviors.Remove(b));
        
        var services = new ServiceCollection();

        services.AddSingleton<IMapper, Mapper>(_ => new Mapper(new MapperConfiguration(AutoMapperSetup.SetupApiMapperProfiles)));

        services.ConfigureServiceLocator();
        Mapper = ServiceLocator.GetService<IMapper>()!;
    }
}