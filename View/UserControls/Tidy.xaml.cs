using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static TidyPDF.MainWindow;
using static TidyPDF.Models.MainWindow;

namespace TidyPDF
{
    /// <summary>
    /// Interaction logic for Tidy.xaml
    /// </summary>
    public partial class Tidy : UserControl
    {
        private readonly FileHelper _fileHelper;

        public Tidy()
        {
            InitializeComponent();
            _fileHelper = new FileHelper();
            GetFiles();
        }

        private void GetFiles()
        {
            var simpleFiles = _fileHelper.GetFiles();
            var files = simpleFiles.Select(x => new PdfFile
            {
                Name = x.Name,
                Path = x.Path,
            });
            lbFiles.ItemsSource = files;
        }

        private void MoveFile(string filePath, Quality quality)
        {
            _fileHelper.MoveFile(filePath, quality);
            GetFiles();
        }

        private void NormalizeFileName()
        {
            var fileName = renameFileTxtBox.Text;
            fileName = _fileHelper.NormalizeFileName(fileName);
            renameFileTxtBox.Text = fileName;
        }

        private void RenameFile()
        {
            var newName = renameFileTxtBox.Text;
            var file = (PdfFile)lbFiles.SelectedItem;
            if (_fileHelper.TryRenameFile(file?.Path, newName))
            {
                GetFiles();
            }
        }

        private void RemoveFile(string filePath)
        {
            _fileHelper.RemoveFile(filePath);
            GetFiles();
        }

        #region Events

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            GetFiles();
        }

        private void renameFileTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RenameFile();
                lbFiles.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                var file = (PdfFile)lbFiles.SelectedItem;
                if (file == null)
                    return;
                lbFiles.Focus();
            }
            else if (e.Key == Key.F1)
            {
                NormalizeFileName();
            }
        }

        private void lbFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListBox)sender;
            var file = (PdfFile)item.SelectedItem;
            if (file == null)
                return;

            renameFileTxtBox.Text = file.Name;

            var tempFilePath = _fileHelper.CreateTempFile(file.Path);
            if (tempFilePath != null)
                webViewer.Source = new Uri(tempFilePath);
        }

        private void lbFiles_KeyDown(object sender, KeyEventArgs e)
        {
            var item = (ListBox)sender;
            var file = (PdfFile)item.SelectedItem;

            if (e.Key == Key.F2)
            {
                renameFileTxtBox.Focus();
                renameFileTxtBox.SelectAll();
            }
            else if (e.Key == Key.NumPad0)
            {
                Debug.WriteLine("Déplacer dans 'Excellent'");
                MoveFile(file.Path, Quality.Excellent);
            }
            else if (e.Key == Key.NumPad1)
            {
                Debug.WriteLine("Déplacer dans 'Bon'");
                MoveFile(file.Path, Quality.Good);
            }
            else if (e.Key == Key.NumPad2)
            {
                Debug.WriteLine("Déplacer dans 'Moyen'");
                MoveFile(file.Path, Quality.Average);
            }
            else if (e.Key == Key.NumPad3)
            {
                Debug.WriteLine("Déplacer dans 'Mauvais'");
                MoveFile(file.Path, Quality.Bad);
            }
            else if (e.Key == Key.Delete)
            {
                Debug.WriteLine("Supprimer");
                RemoveFile(file.Path);
            }
        }

        private void renameFileBtn_Click(object sender, RoutedEventArgs e)
        {
            RenameFile();
        }

        private void normalizeFileBtn_Click(object sender, RoutedEventArgs e)
        {
            NormalizeFileName();
        }

        #endregion Events
    }
}