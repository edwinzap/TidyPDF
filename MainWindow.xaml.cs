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
        private const string _path = @"E:\Chants chrétiens\#A trier\Chants chrétiens";

        public MainWindow()
        {
            InitializeComponent();
            GetFiles();
        }

        private void GetFiles()
        {
            var files = Directory.GetFiles(_path)
                .Select(path => new PdfFile
                {
                    Name = Path.GetFileNameWithoutExtension(path),
                    Path = path
                })
                .ToList();

            lbFiles.ItemsSource = files;
        }

        private void OnListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListBox)sender;
            var file = (PdfFile)item.SelectedItem;
            //webViewer.Source = new Uri(file.Path);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var item = (ListBox)sender;
            var file = (PdfFile)item.SelectedItem;

            if (e.Key == Key.F2) 
            {
                file.IsEditing = true;
            }
            else if (e.Key == Key.Enter && file.IsEditing)
            {
                RenameFile(file.Path, file.Name);
                file.IsEditing = false;
                GetFiles();
            }
            else if (e.Key == Key.NumPad0)
            {
                Debug.WriteLine("Déplacer dans 'Excellent'");
                MoveFile()
            }
            else if (e.Key == Key.NumPad1)
            {
                Debug.WriteLine("Déplacer dans 'Bon'");
            }
            else if (e.Key == Key.NumPad2)
            {
                Debug.WriteLine("Déplacer dans 'Moyen'");
            }
            else if (e.Key == Key.NumPad3)
            {
                Debug.WriteLine("Déplacer dans 'Mauvais'");
            }
        }

        
    }
}