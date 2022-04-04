using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TidyPDF
{
    public class FileHelper
    {
        private readonly QualityHelper _qualityHelper;

        public FileHelper()
        {
            _qualityHelper = new QualityHelper();
        }

        public void MoveFile(string filePath, Quality quality)
        {
            var directoryName = _qualityHelper.GetQualityDirectoryName(quality);
            File.Move(oldPath, newPath);
        }

        public void RenameFile(string? oldPath, string? newName)
        {
            if (string.IsNullOrEmpty(oldPath) || string.IsNullOrEmpty(newName))
                return;

            if (newName.IndexOfAny(Path.GetInvalidFileNameChars()) > 0 && File.Exists(oldPath))
                return;

            var extension = Path.GetExtension(oldPath);
            newName += "." + extension;

            var relativePath = Path.GetDirectoryName(oldPath);
            var newPath = Path.Combine(relativePath!, newName);
            File.Move(oldPath, newPath);
        }
    }
}
