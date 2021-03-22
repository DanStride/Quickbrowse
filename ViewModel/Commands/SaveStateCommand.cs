using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VSA_Viewer.Classes;

namespace VSA_Viewer.ViewModel.Commands
{
    public class SaveStateCommand : ICommand
    {
        public ImageSetVM VM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SaveStateCommand(ImageSetVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
             if (VM.CurrentImage != null && VM.ImageSet.FolderPath != null)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            DatabaseHandler dh = new DatabaseHandler();
            dh.UpdateState(VM.ImageSet.FolderPath, VM.CurrentImage.UriSource.LocalPath, VM.SavePath);
        }
    }
}
