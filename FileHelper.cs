using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
                .OrderBy(x => x.Name)
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

            int increment = 2;
            while (File.Exists(targetFilePath))
            {
                targetFilePath = $"{fileName} ({increment++})";
            }
            File.Move(filePath, targetFilePath);
        }

        public void RemoveFile(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            File.Delete(filePath);
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
            var relativePath = Path.GetDirectoryName(oldPath);
            var newPath = Path.Combine(relativePath!, newName) + extension;

            int increment = 2;
            while (File.Exists(newPath) && oldPath.ToLower() != newPath.ToLower())
            {
                newPath = Path.Combine(relativePath!, $"{newName} ({increment++})") + extension;
            }
            File.Move(oldPath, newPath);
            return true;
        }

        public string NormalizeFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            fileName = fileName.Trim();
            fileName = fileName.ToLower();
            fileName = fileName.Replace("_", " ");

            foreach (var item in Constants.ReplaceWords)
            {
                fileName = fileName.Replace(item.Key, item.Value, StringComparison.CurrentCultureIgnoreCase);
            }

            foreach (var word in Constants.ProperCaseWords)
            {
                var newWord = word.ToArray();
                int index = newWord[0] == ' ' ? 1 : 0;
                newWord[index] = char.ToUpper(newWord[index]);

                fileName = fileName.Replace(word!, string.Concat(newWord));
            }

            var whiteSpacesRegex = new Regex(@"\s+");
            fileName = whiteSpacesRegex.Replace(fileName, " ");

            var numberRegex = new Regex(@"[\[\( ]+([a-zA-Z]{1,3}) ?([0-9]+(?:-[0-9]+){0,2})[\]\)]?");
            if (numberRegex.IsMatch(fileName))
            {
                var match = numberRegex.Match(fileName);
                var letters = match.Groups[1].Value.ToUpper();
                var numbers = match.Groups[2].Value;
                var newValue = $" [{letters}{numbers}]";
                fileName = numberRegex.Replace(fileName, newValue);
            }
            fileName = char.ToUpper(fileName[0]) + fileName[1..];
            return fileName;
        }
    }
}