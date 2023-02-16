namespace Utils
{
    public static class StringUtils
    {
        public static string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex
                .Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled)
                .Trim();
        }
    }
}