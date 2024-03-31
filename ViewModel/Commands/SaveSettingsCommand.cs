using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VSA_Viewer.Classes;

namespace VSA_Viewer.ViewModel.Commands
{
    public class SaveSettingsCommand : ICommand
    {
        public ImageSetVM VM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SaveSettingsCommand(ImageSetVM vm)
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
            try
            {
                dh.UpdateSettings(new Settings
                {
                    settingName = "LoadStateOnStartup",
                    settingValue = "0",
                    enabled = VM.LoadStateOnStartup
                });
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("An error occurred in SaveStateCommand: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                dh.AddErrorLogEntry(e);
            }
            
        }
    }
}
