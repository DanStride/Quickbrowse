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
    public class LoadStateCommand : ICommand
    {
        public ImageSetVM VM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public LoadStateCommand(ImageSetVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DatabaseHandler dh = new DatabaseHandler();
            State newState = dh.GetState();
            if (newState.currentImage != null && newState.folderPath != null)
            {
                VM.LoadFromState(newState);
            }
        }
    }
}
