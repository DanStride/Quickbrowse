using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace VSA_Viewer.Classes
{
    [Table("App_State")]
    public class App_State
    {
        [PrimaryKey]
        [Column("Folder_Path")]
        public string folderPath { get; set; }
                
        [Column("Current_Image")]
        public string currentImage { get; set; }

        [Column("Save_Path")]
        public string savePath { get; set; }
        [Column("Auto_Load")]
        public bool autoLoad { get; set; }
    }

    [Table("Browsing_Log")]
    public class Browsing_Log
    {
        [PrimaryKey, AutoIncrement]
        [Column("ID")]
        public int id { get; set; }

        [Column("Path")]
        public string path { get; set; }

        [Column("Date")]
        public string date { get; set; }
        [Column("Saved")]
        public bool saved {  get; set; }
    }

    [Table("Error_Log")]
    public class Error_Log
    {
        [PrimaryKey, AutoIncrement]
        [Column("ID")]
        public int id { get; set; }

        [Column("Message")]
        public string messsage { get; set; }

        [Column("Date")]
        public string date { get; set; }
    }
}
