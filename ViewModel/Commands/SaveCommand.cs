using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VSA_Viewer.Classes;

namespace VSA_Viewer.ViewModel.Commands
{
    public class SaveCommand : ICommand
    {
        public ImageSetVM VM { get; set;}

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SaveCommand(ImageSetVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            if (VM.CurrentImage != null && VM.SavePath != null)
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
            Repo repo = new Repo(); 
            repo.SaveImage(VM);
        }
    }
}
