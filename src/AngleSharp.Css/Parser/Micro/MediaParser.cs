namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents extensions to for media values.
    /// </summary>
    public static class MediaParser
    {
        internal static IEnumerable<CssMedium> Parse(String str, IFeatureValidatorFactory factory)
        {
            var source = new StringSource(str);
            var result = source.ParseMedia(factory);
            return source.IsDone ? result : null;
        }

        /// <summary>
        /// Parses the CSS media value.
        /// </summary>
        public static IEnumerable<CssMedium> ParseMedia(this StringSource source, IFeatureValidatorFactory factory)
        {
            var current = source.SkipSpacesAndComments();
            var media = new List<CssMedium>();

            while (!source.IsDone)
            {
                if (media.Count > 0)
                {
                    if (current != Symbols.Comma)
                    {
                        return null;
                    }

                    source.SkipCurrentAndSpaces();
                }

                media.Add(source.ParseMedium(factory));
                current = source.SkipSpacesAndComments();
            }

            return media;
        }
    }
}
