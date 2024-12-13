using AutoMapper;
using Serilog;

namespace CodingBlog.Infrastructure.Extensions;

public static class ObjectExtensions
{
    public static TDestination MapTo<TDestination>(this object obj) where TDestination : notnull
    {
        try
        {
            var mapper = ServiceLocator.GetService<IMapper>();
            if (mapper is null)
                throw new NullReferenceException("Error while locating service for mapper instance!");

            return mapper.Map<TDestination>(obj);
        }
        catch(Exception ex)
        {
            Log.Error(ex, $"Error while mapping from type '{obj.GetType()}' to type '{typeof(TDestination)}'");
            throw;
        }
    }
}