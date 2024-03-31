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
using VSA_Viewer.Properties;

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
                    db.CreateTable<Settings>();
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

        public void UpdateSettings(Settings newSetting)
        {
            try
            {
                if (newSetting != null)
                {
                    using (var db = new SQLiteConnection(dbPath))
                    {
                        var existingSettings = db.Query<Settings>("SELECT * FROM Settings WHERE Setting_Name = ?", newSetting.settingName);

                        if (existingSettings.Any())
                        {

                            var settingToUpdate = existingSettings.First();

                            settingToUpdate.settingValue = newSetting.settingValue;
                            settingToUpdate.enabled = newSetting.enabled;

                            db.Update(settingToUpdate);
                        }
                        else
                        {
                            db.Insert(newSetting);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("An error occurred: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                AddErrorLogEntry(e);
            }
        }

        public List<Settings> GetSettings()
        {
            var settings = new List<Settings>();
            try
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    settings = db.Query<Settings>("SELECT * FROM Settings");
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("An error occurred: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                AddErrorLogEntry(e);
            }
            return settings;
        }

        public void AddDefaultSettings()
        {
            try
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    db.Insert(new Settings
                    {
                        settingName = "LoadStateOnStartup",
                        settingValue = "0",
                        enabled = false
                    });
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("An error occurred: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                AddErrorLogEntry(e);
            }
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

        public string GetMostRecentImageFromLog()
        {
            string imagePath = "";
            try
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    var result = db.Table<Browsing_Log>().LastOrDefault();

                    if (result != null)
                    {
                        imagePath = result.path;
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Oh god, an error trying to log the error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return imagePath;
        }

    }
}
