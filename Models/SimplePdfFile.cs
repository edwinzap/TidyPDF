namespace TidyPDF.Models
{
    public class SimplePdfFile
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Quality Quality { get; set; } = Quality.Unknown;
    }
}