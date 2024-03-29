using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using VSA_Viewer.Classes;
using VSA_Viewer.ViewModel;

namespace VSA_Viewer
{
    /// <summary>
    /// Interaction logic for FullScreenWindow.xaml
    /// </summary>
    public partial class FullScreenWindow : Window
    {
        public ImageSetVM VM { get; set; }

        public FullScreenWindow(ImageSetVM vm)
        {
            InitializeComponent();
            VM = vm;
            this.DataContext = VM;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            if (e.Key == Key.Down || e.Key == Key.S)
            {
                VM.NextImage();
            }
            if (e.Key == Key.Up || e.Key == Key.W)
            {
                VM.PreviousImage();
            }
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                Repo repo = new Repo();
                repo.SaveImage(VM);
            }
            if (e.Key == Key.Right || e.Key == Key.D)
            {
                VM.ChangeToNextFolder();
            }
            if (e.Key == Key.Left || e.Key == Key.A)
            {
                VM.ChangeToPreviousFolder();
            }
            if (e.Key == Key.NumPad0 || e.Key == Key.LeftShift)
            {
                VM.ChangeToRandomImage();
            }
        }
    }
}
