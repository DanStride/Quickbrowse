using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSA_Viewer.Model
{
    public class ImageSet : INotifyPropertyChanged
    {
        private ObservableCollection<string> itemsInFolder { get; set; }

        public ObservableCollection<string> ItemsInFolder
        { 
            get { return itemsInFolder; }
            set
            {
                itemsInFolder = value;
                OnPropertyChanged("ItemsInFolder");
            }
        }

        private ObservableCollection<string> imagesInFolder;

        public ObservableCollection<string> ImagesInFolder
        {
            get { return imagesInFolder; }
            set 
            { 
                imagesInFolder = value;
                OnPropertyChanged("ImagesInFolder");
            }
        }


        private ObservableCollection<string> foldersInFolder;

        public ObservableCollection<string> FoldersInFolder
        {
            get { return foldersInFolder; }
            set 
            { 
                foldersInFolder = value;
                OnPropertyChanged("FoldersInFolder");
            }
        }



        private string folderPath { get; set; }

        public string FolderPath
        {
            get { return folderPath; }
            set
            {
                folderPath = value;
                OnPropertyChanged("FolderPath");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
