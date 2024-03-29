using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using System.Windows.Forms;
using System.Windows;

namespace VSA_Viewer.Classes
{
    public class DatabaseHandler
    {
        private string dbPath = ($"{Directory.GetCurrentDirectory()}\\Database.db");

        public string DBPath
        {
            get { return dbPath; }
            set
            {
                dbPath = value;
            }
        }

        public DatabaseHandler()
        {
            try
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    db.CreateTable<App_State>();
                    db.CreateTable<Browsing_Log>();
                    db.CreateTable<Error_Log>();
                }
            }
            catch (Exception e) 
            {
                System.Windows.MessageBox.Show("An error occurred: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                AddErrorLogEntry(e);
            }
        }

        public void UpdateState(string path, string image, string saveDir)
        {
            try
            {
                if (path != null && image != null)

                    using (var db = new SQLiteConnection(dbPath))
                    {
                        var state = new App_State
                        {
                            folderPath = path,
                            currentImage = image,
                            savePath = saveDir
                        };

                        db.Delete(state);
                        db.Insert(state);
                    }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("An error occurred: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                AddErrorLogEntry(e);
            }
        }

        public App_State GetState()
        {
            var newState = new App_State();
            try
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    var state = db.Query<App_State>("SELECT * FROM App_State LIMIT 1").FirstOrDefault();

                    if (state != null)
                    {
                        newState.currentImage = state.currentImage;
                        newState.folderPath = state.folderPath;
                        newState.savePath = state.savePath;
                        newState.autoLoad = state.autoLoad;
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("An error occurred: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                AddErrorLogEntry(e);
            }
            return newState;
        }

        public void AddBrowsingLogEntry(Browsing_Log log)
        {
            try
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    var result = db.Insert(log);
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("An error occurred: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                AddErrorLogEntry(e);
            }

        }

        public void AddErrorLogEntry(Exception error)
        {
            try
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    var result = db.Insert(new Error_Log
                    {
                        messsage = error.Message,
                        date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                    });
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Oh god, an error trying to log the error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SetLoadStateOnStartup(bool state)
        {
            try
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    var appState = db.Table<App_State>().FirstOrDefault();
                    if (appState != null)
                    {
                        appState.autoLoad = state;
                        db.Update(appState);
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("An error occurred: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                AddErrorLogEntry(e);
            }
        }
    }
}
