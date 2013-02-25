using System;
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
            
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void toggle_col_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            int result;
            //Numeric values only
            if (!(int.TryParse(e.Text, out result)))
            {
                e.Handled = true;
            }
            else
            {
                //Stop user from entering values out of range
                int.TryParse(toggle_col.Text + e.Text, out result);

                if (result >= MainViewModel.getBoardWidth()) e.Handled = true;
            }
        }

        private void toggle_row_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            int result;
            //Numeric values only
            if (!(int.TryParse(e.Text, out result)))
            {
                e.Handled = true;
            }
            else
            {
                //Stop user from entering values out of range
                int.TryParse(toggle_row.Text + e.Text, out result);

                if (result >= MainViewModel.getBoardHeight()) e.Handled = true;
            }
        }
    }
}