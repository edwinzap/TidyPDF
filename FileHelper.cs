using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static TidyPDF.MainWindow;

namespace TidyPDF
{
    public class FileHelper
    {
        private readonly QualityHelper _qualityHelper;

        public FileHelper()
        {
            _qualityHelper = new QualityHelper();
        }

        public List<PdfFile> GetFiles()
        {
            var files = Directory.GetFiles(Constants.WorkingDirectoryPath)
                .Where(x => Path.GetExtension(x) == ".pdf" && Path.GetFileNameWithoutExtension(x) != "temp")
                .Select(path => new PdfFile
                {
                    Name = Path.GetFileNameWithoutExtension(path),
                    Path = path
                })
                .ToList();
            return files;
        }


        public void MoveFile(string filePath, Quality quality)
        {
            if (!File.Exists(filePath))
                return;

            var directoryName = _qualityHelper.GetQualityDirectoryName(quality);
            var targetPath = Path.Combine(Constants.TargetDirectoryPath, directoryName);
            var fileName = Path.GetFileName(filePath);
            var targetFilePath = Path.Combine(targetPath, fileName);
            File.Move(filePath, targetFilePath);
        }

        public string? CreateTempFile(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            var relativePath = Path.GetDirectoryName(filePath);
            relativePath = Path.Combine(relativePath, "temp");
            Directory.CreateDirectory(relativePath);
            var newFilePath = Path.Combine(relativePath, "temp.pdf");
            try
            {
                File.Copy(filePath, newFilePath, true);
            }
            catch (Exception)
            {
                newFilePath = Path.Combine(relativePath, "temp1.pdf");
                File.Copy(filePath, newFilePath, true);
            }
            
            return newFilePath;
        }

        public bool TryRenameFile(string? oldPath, string? newName)
        {
            if (!File.Exists(oldPath))
                return false;

            if (string.IsNullOrEmpty(oldPath) || string.IsNullOrEmpty(newName))
                return false;

            if (newName.IndexOfAny(Path.GetInvalidFileNameChars()) > 0 && File.Exists(oldPath))
                return false;

            var extension = Path.GetExtension(oldPath);
            newName += extension;

            var relativePath = Path.GetDirectoryName(oldPath);
            var newPath = Path.Combine(relativePath!, newName);
            File.Move(oldPath, newPath);
            return true;
        }

        public void NormalizeFileName(string fileName)
        {
            fileName = char.ToUpper(fileName[0]) + fileName.Substring(1).ToLower().Trim();
            fileName = fileName.Replace("_", " ");

            foreach (var item in Constants.ReplaceWords)
            {
                fileName = fileName.Replace(item.Key, item.Value);
            }

            var regex = new Regex(@"[\[\( ]+([a-zA-Z]{1,2}) ?([0-9]+)[\]\)]?");
            if (regex.IsMatch(fileName))
            {
                var match = regex.Match(fileName);
                var letters = match.Groups[1].Value.ToUpper();
                var numbers = match.Groups[2].Value;
                var newValue = $"[{letters}{numbers}]";
                fileName = regex.Replace(fileName, newValue);
            }
        }
    }
}
