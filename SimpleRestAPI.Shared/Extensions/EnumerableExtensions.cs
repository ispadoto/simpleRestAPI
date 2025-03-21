using System.Diagnostics.CodeAnalysis;

namespace SimpleRestAPI.Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNotNullAndAny([NotNullWhen(true)] this IEnumerable<object> source)
            => source != null && source.Any();

        public static bool IsNullOrNotAny([NotNullWhen(false)] this IEnumerable<object>? source)
            => source == null || !source.Any();

        public static bool In(this object source, params object[] valids)
            => valids.Contains(source);   
    }
}
