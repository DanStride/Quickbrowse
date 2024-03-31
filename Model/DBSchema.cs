using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace VSA_Viewer.Classes
{
    [Table("Settings")]
    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        [Column("ID")]
        public int id { get; set; }
        [Column("Setting_Name")]
        public string settingName { get; set; }
        [Column("Seting_Value")]
        public string settingValue { get; set; }
        [Column("Enabled")]
        public bool enabled { get; set; }
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

    [Table("Copy_Log")]
    public class Copy_Log
    {
        [PrimaryKey, AutoIncrement]
        [Column("ID")]
        public int id { get; set; }

        [Column("Image_Path")]
        public string imagePath { get; set; }
        [Column("Save_Path")]
        public string savePath { get; set; }
        [Column("Browsing_Log_Ref")]
        public int browsingLogRef { get; set; }
        [Column("Folder_Save")]
        public bool folderSave { get; set; }

        [Column("Date")]
        public string date { get; set; }
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
