using System.Text.RegularExpressions;

namespace GeekCliServices.ExtensionMethods
{
    public static class StringExtension
    {
        public static string ToPascalCase(this string value)
        {
            return string.Join('/', value.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(segment => string.Concat(GetWords(segment).Select(word => char.ToUpperInvariant(word[0]) + word[1..].ToLowerInvariant()))));
        }

        public static string ToKebabCase(this string value)
        {
            return string.Join("-", GetWords(value).Select(word => word.ToLowerInvariant()));
        }

        private static string[] GetWords(string value)
        {
            var words = Regex.Replace(value.Trim(), "([a-z0-9])([A-Z])", "$1 $2");
            words = Regex.Replace(words, "([A-Z])([A-Z][a-z])", "$1 $2");
            words = Regex.Replace(words, "[^A-Za-z0-9]+", " ");
            return words.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
