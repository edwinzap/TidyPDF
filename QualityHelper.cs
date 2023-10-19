using System;
using System.Collections.Generic;
using System.Linq;

namespace TidyPDF
{
    public class QualityHelper
    {
        public static string GetQualityDirectoryName(Quality quality)
        {
            if (QualityDirectories.TryGetValue(quality, out string directory))
            {
                return directory;
            }
            throw new NotImplementedException("This quality does not exist");
        }

        public static Quality GetQualityFromPath(string path)
        {
            KeyValuePair<Quality, string>? quality = QualityDirectories
                .Where(x => path.Contains(x.Value))
                .FirstOrDefault();

            if (quality.HasValue)
                return quality.Value.Key;
            else
                return Quality.Unknown;
        }

        public static Dictionary<Quality, string> QualityDirectories = new()
        {
            { Quality.Excellent, "0 Excellent" },
            { Quality.Good, "1 Bon" },
            { Quality.Average, "2 Moyen" },
            { Quality.Bad, "3 Mauvais" },
        };
    }

    public enum Quality
    {
        Excellent,
        Good,
        Average,
        Bad,
        Unknown,
    }
}