using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace BIT.Tools.Extensions
{
    public static class StringExtension
    {
        public static String ToSafeString(this object val)
        {
            return (val ?? String.Empty).ToString();
        }
        public static string ToProperCase(this string text)
        {
            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(text);
        }

        public static String toSlug(this string text)
        {
            String value = text.Normalize(NormalizationForm.FormD).Trim();
            StringBuilder builder = new StringBuilder();

            var valor = text.ToCharArray();
            for (int i = 0; i < valor.Length; i++)
                if (CharUnicodeInfo.GetUnicodeCategory(valor[i]) != UnicodeCategory.NonSpacingMark)
                    builder.Append(valor[i]);

            value = builder.ToString();

            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);

            value = Regex.Replace(Regex.Replace(Encoding.ASCII.GetString(bytes), @"\s{2,}|[^\w]", " ", RegexOptions.ECMAScript).Trim(), @"\s+", "_");

            return value.ToLowerInvariant();
        }
    }
}
