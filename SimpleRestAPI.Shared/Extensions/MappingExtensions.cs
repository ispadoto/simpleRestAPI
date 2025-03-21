using System.Linq.Expressions;
using AgileObjects.AgileMapper;

namespace SimpleRestAPI.Shared.Extensions
{
    public static class MappingExtensions
    {
        public static TSource Clone<TSource>(this TSource source)
        {
            return Mapper.DeepClone(source);
        }

        public static TDestination Map<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map(source).ToANew<TDestination>();
        }

        public static TDestination MapIgnore<TSource, TDestination>(this TSource source, params Expression<Func<TDestination, object>>[] targetMembers)
        {
            Mapper.WhenMapping
                .From<TSource>()
                .To<TDestination>()
                .Ignore(targetMembers);

            return source.Map<TDestination>();
        }

        public static TDestination Map<TDestination>(this object source)
        {
            return Mapper.Map(source).ToANew<TDestination>();
        }

        public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source).OnTo(destination);
        }

        public static IQueryable<TDestination> Project<TSource, TDestination>(this IQueryable<TSource> queryable)
        {
            return queryable.Project().To<TDestination>();
        }
    }
}
