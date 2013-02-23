using System.Windows;
using Game_of_Life.ViewModel;


namespace Game_of_Life
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = ViewModel
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
    }
}