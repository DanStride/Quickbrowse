using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace VSA_Viewer.Classes
{
    [Table("State")]
    public class State
    {
        [PrimaryKey]
        [Column("folderPath")]
        public string folderPath { get; set; }
                
        [Column("currentImage")]
        public string currentImage { get; set; }

        [Column("savePath")]
        public string savePath { get; set; }
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
