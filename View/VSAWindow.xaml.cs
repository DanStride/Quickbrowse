using Microsoft.Win32;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VSA_Viewer.Classes;
using VSA_Viewer.ViewModel;
using VSA_Viewer.ViewModel.Commands;

namespace VSA_Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (Resources["vm"] is ImageSetVM imageSetVM)
            {
                // Setting the DataContext of the window to the ViewModel
                this.DataContext = imageSetVM;
            }
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void filesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filesListView.ScrollIntoView(filesListView.SelectedItem);
        }

        private void closeApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void filesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Since DataContext is already set to ImageSetVM, we can cast it directly
            if (DataContext is ImageSetVM imageSetVM)
            {
                if (imageSetVM.SubFolderCommand.CanExecute(null))
                {
                    imageSetVM.SubFolderCommand.Execute(null);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseHandler db = new DatabaseHandler();
            try
            {
                //Settings settings = db.GetSettings();
                //if (settings.autoLoad == true)
                //{
                //    if (DataContext is ImageSetVM imageSetVM)
                //    {
                //        if (imageSetVM.LoadStateCommand.CanExecute(null))
                //        {
                //            imageSetVM.LoadStateCommand.Execute(null);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                db.AddErrorLogEntry(ex);
            }
            
        }
    }
}
