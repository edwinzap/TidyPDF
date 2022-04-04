using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TidyPDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FileHelper _fileHelper;

        public MainWindow()
        {
            InitializeComponent();
            _fileHelper = new FileHelper();
            GetFiles();
        }

        private void GetFiles()
        {
            var files = _fileHelper.GetFiles();
            lbFiles.ItemsSource = files;
        }

        private void OnListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListBox)sender;
            var file = (PdfFile)item.SelectedItem;
            if (file == null)
                return;

            var tempFilePath = _fileHelper.CreateTempFile(file.Path);
            if (tempFilePath != null)
                webViewer.Source = new Uri(tempFilePath);
        }

        private void ListBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            var item = (ListBox)sender;
            var file = (PdfFile)item.SelectedItem;

            if (e.Key == Key.F2) 
            {
                file.IsEditing = true;
            }
            else if (e.Key == Key.Enter && file.IsEditing)
            {
                _fileHelper.RenameFile(file.Path, file.Name);
                file.IsEditing = false;
                GetFiles();
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
        }

        private void MoveFile(string filePath, Quality quality)
        {
            _fileHelper.MoveFile(filePath, quality);
            GetFiles();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}