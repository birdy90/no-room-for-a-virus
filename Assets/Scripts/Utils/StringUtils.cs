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

        public static string TimeToString(float time)
        {
            return $"{time / 60:F0}:{time % 60:00}";
        }
    }
}