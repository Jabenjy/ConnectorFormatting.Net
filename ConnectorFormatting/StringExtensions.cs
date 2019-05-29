using System.Globalization;
using System.Text.RegularExpressions;

namespace ConnectorFormating
{
    public static class ConnectorStringExtensions
    {
        /// <summary>
        /// Remove square braces from a string.
        /// </summary>
        /// <param name="stringToFormat"></param>
        /// <returns></returns>
        public static string RemoveSquareBraces(this string stringToFormat) => stringToFormat.Replace("[", "").Replace("]", "");

        /// <summary>
        /// Remove spaces from a string.
        /// </summary>
        /// <param name="stringToFormat"></param>
        /// <returns></returns>
        public static string RemoveSpaces(this string stringToFormat) => stringToFormat.Replace(" ", "");

        /// <summary>
        /// Remove underscores from the beginning of a string.
        /// </summary>
        /// <param name="stringToFormat"></param>
        /// <returns></returns>
        public static string RemoveLeadingUnderscores(this string stringToFormat) => Regex.Replace(stringToFormat, @"^_", "");

        /// <summary>
        /// Changes periods and underscores to spaces.
        /// </summary>
        /// <param name="stringToFormat"></param>
        /// <returns></returns>
        public static string PeriodsUnderscoresToSpaces(this string stringToFormat) => Regex.Replace(stringToFormat, @"_|\.", " ");

        /// <summary>
        /// Title cases words. Works with acronyms.
        /// </summary>
        /// <param name="stringToFormat"></param>
        /// <returns></returns>
        public static string TitleCaseWords(this string stringToFormat) => Regex.Replace(stringToFormat, @"(?:\b)(\w)",
                (match) => match.Value.ToUpper());

        /// <summary>
        /// Add spaces between each word.
        /// </summary>
        /// <param name="stringToFormat"></param>
        /// <returns></returns>
        public static string SplitWords(this string stringToFormat) => Regex.Replace(stringToFormat, @"([a-z])([A-Z])", "$1 $2");

        public static string SplitAcronym(this string stringToFormat) => Regex.Replace(stringToFormat, @"([A-Z]+)([A-Z])([a-z])", "$1 $2$3");

        /// <summary>
        /// Change id and Id at the end of words to ID.
        /// </summary>
        /// <param name="stringToFormat"></param>
        /// <returns></returns>
        public static string FormatDisplayNameIds(this string stringToFormat) => Regex.Match(stringToFormat, @"paid|Paid\b").Success ? Regex.Replace(stringToFormat, @"id\b|\bId\b", "ID") : stringToFormat;

        /// <summary>
        /// Change id and ID at the end of words to Id.
        /// </summary>
        /// <param name="stringToFormat"></param>
        /// <returns></returns>
        public static string FormatSystemFieldIds(this string stringToFormat) => Regex.Replace(stringToFormat, @"id\b|ID\b", "Id");
    }
}
