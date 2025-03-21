using System.Diagnostics.CodeAnalysis;

namespace SimpleRestAPI.Shared.Extensions;

public static class StringExtensions
{
    public static bool HasValue([NotNullWhen(true)] this string? source)
        => source != null && !string.IsNullOrWhiteSpace(source);

    public static string TrimAndUppercase(this string? source)
        => string.IsNullOrWhiteSpace(source)
            ? string.Empty
            : source.Trim().ToUpper();

    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? source)
        => string.IsNullOrWhiteSpace(source);
}
