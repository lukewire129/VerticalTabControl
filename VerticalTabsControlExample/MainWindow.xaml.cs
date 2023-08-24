using System.Windows;
using VerticalTabsControlExample.ViewModel;

namespace VerticalTabsControlExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent ();
            this.DataContext = new MainWindowViewModel ();
        }
    }
}
