using System;

namespace TidyPDF
{
    public class QualityHelper
    {
        public string GetQualityDirectoryName(Quality quality)
        {
            return (quality) switch
            {
                Quality.Excellent => "Excellent",
                Quality.Good => "Bon",
                Quality.Average => "Moyen",
                Quality.Bad => "Mauvais",
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