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
}
