using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using TidyPDF.Models;

namespace TidyPDF
{
    /// <summary>
    /// Interaction logic for Duplicate.xaml
    /// </summary>
    public partial class Duplicate : UserControl
    {
        private readonly FileHelper _fileHelper;

        public Duplicate()
        {
            InitializeComponent();
            _fileHelper = new FileHelper();
        }

        private void GetFiles()
        {
            var files = _fileHelper.GetFiles(true);

            var duplicates = new List<DuplicateFiles>();
            foreach (var file in files)
            {
                var regex = new Regex(@"((\[|\().*(\]|\)))", RegexOptions.IgnoreCase);
                string name = regex.Replace(file.Name, "").Trim();

                var existingDuplicates = duplicates.FirstOrDefault(x => x.Name == name);
                file.Quality = QualityHelper.GetQualityFromPath(file.Path);
                if (existingDuplicates != null)
                {
                    existingDuplicates.Files.Add(file);
                }
                else
                {
                    var newDuplicates = new DuplicateFiles
                    {
                        Name = name,
                    };
                    newDuplicates.Files.Add(file);
                    duplicates.Add(newDuplicates);
                }
            }
            duplicates = duplicates.Where(x => x.Files.Count > 1).ToList();
            lbNames.ItemsSource = duplicates.OrderBy(x => x.Name).ToList();
        }

        private void MoveDuplicateFile(string path, bool shouldRemove)
        {
            _fileHelper.MoveDuplicateFile(path, shouldRemove);
            var item = lbFiles.SelectedItem as SimplePdfFile;
            (lbFiles.ItemsSource as List<SimplePdfFile>).Remove(item);
            lbFiles.Items.Refresh();
        }

        private void ShowFileInExplorer(string path)
        {
            if (!File.Exists(path))
                return;

            // combine the arguments together
            // it doesn't matter if there is a space after ','
            string argument = "/select, \"" + path + "\"";

            Process.Start("explorer.exe", argument);
        }

        #region Events

        private void lbNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListBox)sender;
            var duplicates = (DuplicateFiles)item.SelectedItem;

            if (duplicates == null)
                return;

            lbFiles.ItemsSource = duplicates.Files.OrderBy(x => x.Name).ToList();
        }

        private void lbFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListBox)sender;
            var file = (SimplePdfFile)item.SelectedItem;

            if(file?.Path != null)
            {
                var tempFilePath = _fileHelper.CreateTempFile(file.Path);
                if (tempFilePath != null)
                    webViewer.Source = new Uri(tempFilePath);
            }
            else
            {
                webViewer.Navigate((Uri?)null);
            }
        }

        private void lbFiles_KeyDown(object sender, KeyEventArgs e)
        {
            var item = (ListBox)sender;
            var file = (SimplePdfFile)item.SelectedItem;

            switch (e.Key)
            {
                case Key.Delete:
                    Debug.WriteLine("Supprimer");
                    MoveDuplicateFile(file.Path, true);
                    break;

                case Key.NumPad0:
                    MoveDuplicateFile(file.Path, false);
                    break;

                case Key.Enter:
                    ShowFileInExplorer(file.Path);
                    break;
            }
        }

        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                GetFiles();
            }
        }

        #endregion Events
    }
}