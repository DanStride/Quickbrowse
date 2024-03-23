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
    public class SubFolderCommand : ICommand
    {
        public ImageSetVM VM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SubFolderCommand(ImageSetVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            string lastSegment = "";

            if (VM.SelectedImageUri != null && VM.SelectedImageUri.Segments.Length > 0)
            {
                lastSegment = VM.SelectedImageUri.Segments[VM.SelectedImageUri.Segments.Length - 1];
            }


            if (lastSegment.Contains('.'))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public void Execute(object parameter)
        {
            VM.ChangeToSubFolder();

        }
    }
}
