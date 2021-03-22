using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace VSA_Viewer.Classes
{
    public class DatabaseHandler
    {
        private string dbPath = ($"{Directory.GetCurrentDirectory()}\\quickbrowse.db");

        public string DBPath
        {
            get { return dbPath; }
            set
            {
                dbPath = value;
            }
        }

        private SQLiteConnection _db;

        public DatabaseHandler()
        {
            _db = new SQLiteConnection(DBPath);
            _db.CreateTable<State>();
        }

        public void UpdateState(string path, string image, string saveDir)
        {
            if (path != null && image != null)
            {
                var state = new State
                {
                    folderPath = path,
                    currentImage = image,
                    savePath = saveDir
                };

                _db.Delete(state);
                _db.Insert(state);
            }
        }

        public State GetState()
        {
            var newState = new State();
            var state = _db.Query<State>("SELECT * FROM State");

            foreach (var s in state)
            {
                newState.currentImage = s.currentImage;
                newState.folderPath = s.folderPath;
                newState.savePath = s.savePath;
            }

            return newState;
        }
    }
}
