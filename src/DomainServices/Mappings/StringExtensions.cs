namespace ShoppingLikeFiles.DomainServices.Mappings;

internal static class StringExtensions
{
    public static string ToCsv(this IEnumerable<string> list)
    {
        string result = string.Empty;
        foreach (var item in list)
        {
            result += item + ";";
        }

        return result;
    }
}
