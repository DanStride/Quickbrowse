using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VSA_Viewer.Classes;

namespace VSA_Viewer.ViewModel.Commands
{
    public class ParentFolderCommand : ICommand
    {
        public ImageSetVM VM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ParentFolderCommand(ImageSetVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            if (Directory.GetParent(VM.ImageSet.FolderPath) == null)
            {
                return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            VM.ChangeToParentFolder();

        }
    }
}
