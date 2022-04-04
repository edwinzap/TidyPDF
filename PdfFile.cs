using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TidyPDF
{
    public partial class MainWindow
    {
        public class PdfFile:INotifyPropertyChanged
        {
            public string Name { get; set; }
            public string Path { get; set; }
            
            private bool isEditing;
            public bool IsEditing
            {
                get { return isEditing; }
                set { isEditing = value; OnPropertyChanged(); }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName] string? name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}