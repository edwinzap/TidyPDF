using System.Collections.Generic;

namespace TidyPDF.Models
{
    public class DuplicateFiles
    {
        public string Name { get; set; }
        public List<SimplePdfFile> Files { get; set; } = new List<SimplePdfFile>();
    }
}