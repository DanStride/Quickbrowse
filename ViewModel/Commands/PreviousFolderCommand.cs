using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VSA_Viewer.ViewModel.Commands
{
    public class PreviousFolderCommand : ICommand
    {
        public ImageSetVM VM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public PreviousFolderCommand(ImageSetVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            if (VM.ImageSet.FolderPath != null && Directory.Exists(VM.ImageSet.FolderPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            VM.ChangeToPreviousFolder();

        }
    }
}
