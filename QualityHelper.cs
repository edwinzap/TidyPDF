using System;

namespace TidyPDF
{
    public class QualityHelper
    {
        public string GetQualityDirectoryName(Quality quality)
        {
            return (quality) switch
            {
                Quality.Excellent => "0. Excellent",
                Quality.Good => "1. Bon",
                Quality.Average => "2. Moyen",
                Quality.Bad => "3. Mauvais",
                _ => throw new NotImplementedException()
            };
        }
    }

    public enum Quality
    {
        Excellent,
        Good,
        Average,
        Bad
    }
}