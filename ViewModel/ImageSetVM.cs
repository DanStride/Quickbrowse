using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using VSA_Viewer.Classes;
using VSA_Viewer.Model;
using VSA_Viewer.View;
using VSA_Viewer.ViewModel.Commands;


namespace VSA_Viewer.ViewModel
{
    public class ImageSetVM : INotifyPropertyChanged
    {
        public ImageSetVM()
        {
            ImageSet = new ImageSet();
            LoadCommand = new LoadCommand(this);
            SaveCommand = new SaveCommand(this);
            SaveEntireFolderCommand = new SaveEntireFolderCommand(this);
            NextFolderCommand = new NextFolderCommand(this);
            PreviousFolderCommand = new PreviousFolderCommand(this);
            ParentFolderCommand = new ParentFolderCommand(this);
            SubFolderCommand = new SubFolderCommand(this);
            SaveSettingsCommand = new SaveSettingsCommand(this);
            LoadStateCommand = new LoadSettingsCommand(this);
            FullScreenCommand = new FullScreenCommand(this);
            ExitFullScreenCommand = new ExitFullScreenCommand(this);
            RandomImageCommand = new RandomImageCommand(this);
            NextImageCommand = new NextImageCommand(this);
            PreviousImageCommand = new PreviousImageCommand(this);
            SettingsWindowCommand = new SettingsWindowCommand(this);
            SetNewSavePathCommand = new SetNewSavePathCommand(this);
            KeyBindingsWindowCommand = new KeyBindingsWindowCommand(this);
            _db = new DatabaseHandler();
            _repo = new Repo();
            LoadSettingsFromDB();
            LoadState();
        }
        private DatabaseHandler _db;
        private Repo _repo;
        private ImageSet imageSet { get; set; }
        public FullScreenWindow fullScreenWindow { get; set; }
        public SettingsWindow settingsWindow { get; set; }
        public KeyBindingsWindow keyBindingsWindow { get; set; }
        private static readonly string[] IMAGE_TYPES = { ".jpg", ".JPG", ".png", ".PNG", ".bmp", ".BMP", ".jpeg", ".JPEG", ".gif", ".GIF" };

        public LoadCommand LoadCommand { get; set; }
        public SaveCommand SaveCommand { get; set; }
        public SaveEntireFolderCommand SaveEntireFolderCommand { get; set; }
        public NextFolderCommand NextFolderCommand { get; set; }
        public PreviousFolderCommand PreviousFolderCommand { get; set; }
        public ParentFolderCommand ParentFolderCommand { get; set; }
        public SubFolderCommand SubFolderCommand { get; set; }
        public SaveSettingsCommand SaveSettingsCommand { get; set; }
        public LoadSettingsCommand LoadStateCommand { get; set; }
        public FullScreenCommand FullScreenCommand { get; set; }
        public ExitFullScreenCommand ExitFullScreenCommand { get; set; }
        public RandomImageCommand RandomImageCommand { get; set; }
        public NextImageCommand NextImageCommand { get; set; }
        public PreviousImageCommand PreviousImageCommand { get; set; }
        public SettingsWindowCommand SettingsWindowCommand { get; set; }
        public SetNewSavePathCommand SetNewSavePathCommand { get; set; }
        public KeyBindingsWindowCommand KeyBindingsWindowCommand { get; set; }

        // App Settings
        private bool loadStateOnStartup;
        public bool LoadStateOnStartup
        {
            get { return loadStateOnStartup; }
            set
            {
                loadStateOnStartup = value;
                OnPropertyChanged("LoadStateOnStartup");
            }
        }

        private string savePath;
        public string SavePath
        {
            get { return savePath; }
            set
            {
                savePath = value;
                OnPropertyChanged("SavePath");
            }
        }

        public ImageSet ImageSet
        {
            get { return imageSet; }
            set
            {
                imageSet = value;
                OnPropertyChanged("ImageSet");
            }
        }

        public int imageIndex { get; set; }

        

        public static Random rnd = new Random();

        private BitmapImage currentImage;

        public BitmapImage CurrentImage
        {
            get { return currentImage; }
            set
            {
                currentImage = value;
                OnPropertyChanged("CurrentImage");
                _db.AddBrowsingLogEntry(new Browsing_Log
                {
                    path = CurrentImage.ToString(),
                    date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                });
                
            }
        }

        private Uri selectedImageUri;

        public Uri SelectedImageUri
        {
            get { return selectedImageUri; }
            set
            {
                selectedImageUri = value;
                OnPropertyChanged("SelectedImageUri");
                ChangeImage(SelectedImageUri);
            }
        }

        private int selectedItemIndex;

        public int SelectedItemIndex
        {
            get
            {
                return selectedItemIndex;
            }
            set
            {
                selectedItemIndex = value;
                if ((ImageSet.ImagesInFolder != null) && (selectedItemIndex < ImageSet.ImagesInFolder.Count))
                {
                    SelectedItemIsImage = true;
                }
                else
                {
                    SelectedItemIsImage = false;
                }
                OnPropertyChanged("SelectedItemIndex");
            }
        }

        private bool selectedItemIsImage;

        public bool SelectedItemIsImage
        {
            get { return selectedItemIsImage; }
            set
            {
                selectedItemIsImage = value;
            }
        }


        private string selectedFolder;

        public string SelectedFolder
        {
            get { return selectedFolder; }
            set
            {
                selectedFolder = value;
                OnPropertyChanged("SelectedFolder");
            }
        }


        private string previousFolder;
        public string PreviousFolder
        {
            get { return previousFolder; }
            set
            {
                previousFolder = value;
                OnPropertyChanged("PreviousFolder");
            }
        }

        private void LoadSettingsFromDB()
        {
            List<Settings> settings = _db.GetSettings();

            // make this a check for all default settings rather than a count check
            if (settings.Count == 0) {
                _db.AddDefaultSettings();
                settings = _db.GetSettings();
            }

            var loadStateOnStartup = settings.FirstOrDefault(s => s.settingName == "LoadStateOnStartup");
            if (loadStateOnStartup != null)
            {
                LoadStateOnStartup = loadStateOnStartup.enabled;
            }

            var savePath = settings.FirstOrDefault(s => s.settingName == "SavePath");
            if (savePath != null)
            {
                SavePath = savePath.settingValue;
            }
        }

        private void LoadState()
        {
            if (LoadStateOnStartup == true)
            {
                // check last image
                string image = _db.GetMostRecentImageFromLog();
                if (!String.IsNullOrEmpty(image)) 
                {
                    SetImageSet(image);
                }
            }
        }

        public void SetSavePath(string path)
        {

            try
            {
                SavePath = path;
                _db.UpdateSettings(new Settings
                {
                    settingName = "SavePath",
                    settingValue = path,
                    enabled = true
                });

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("An error occurred: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _db.AddErrorLogEntry(e);
            }
        }

        public void SetDBRecordsForSave(string saveType)
        {
            // if single save, add record to copy_history and and give them a ref to browsing_history Set flag on browsin history 
            if (saveType == "single_save")
            {
                string path = SelectedImageUri.ToString();
                _db.AddEntriesForSave(path, SavePath);
            }
            else if (saveType == "folder_save")
            {
                List<string> paths = ImageSet.ImagesInFolder.ToList();
                _db.AddEntriesForFolderSave(paths, SavePath);
            }
            // if folder save, add records to copy history and give them a ref to browsing_history. Set flag on browsin history 
        }

        private ObservableCollection<string> GetImagesInFolder()
        {
            ObservableCollection<string> newImageSet = new ObservableCollection<string>();

            string[] imageArray = Directory.GetFiles(imageSet.FolderPath);

            var fileQuery =
                from image in imageArray
                where IMAGE_TYPES.Contains(Path.GetExtension(image))
                select image;

            foreach (var file in fileQuery)
            {
                newImageSet.Add(file);
            }
            return newImageSet;
        }

        private ObservableCollection<string> GetFoldersInFolder()
        {
            ObservableCollection<string> newFolderSet = new ObservableCollection<string>();

            string[] folderArray = Directory.GetDirectories(imageSet.FolderPath);

            foreach (var folder in folderArray)
            {
                var directoryInfo = new DirectoryInfo(folder);

                bool isSystem = (directoryInfo.Attributes & FileAttributes.System) == FileAttributes.System;

                // Add the folder to the list only if it is not a system folder.
                if (!isSystem)
                {
                    newFolderSet.Add(folder);
                }

            }
            return newFolderSet;
        }


        public void LoadNewFolder()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Load Image";
            ofd.Filter = "Jpg Files (*.jpg)|*.jpg";
            ofd.RestoreDirectory = true;

            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                SetImageSet(ofd.FileName);
            }
        }


        public void SetImageSetForDirectory(string path)
        {
            ImageSet.FolderPath = path;

            ImageSet.ImagesInFolder = GetImagesInFolder();
            ImageSet.FoldersInFolder = GetFoldersInFolder();

            ImageSet.ItemsInFolder = new ObservableCollection<string>(ImageSet.ImagesInFolder.Concat(ImageSet.FoldersInFolder));

            SelectedItemIndex = ImageSet.FoldersInFolder.IndexOf(ImageSet.ItemsInFolder.Where(x => x.Contains(PreviousFolder)).FirstOrDefault());
            if (SelectedItemIndex == -1) 
            {
                SelectedItemIndex = 0;
            }

    



            //SelectedItemIndex = ImageSet.ItemsInFolder.IndexOf(ImageSet.ItemsInFolder.Where(x => x.Contains(CurrentImage.UriSource.LocalPath)).FirstOrDefault());
        }

        public void SetImageSet(string fileName)
        {
            if ((IMAGE_TYPES.Any(i => i.Contains(Path.GetExtension(fileName)))) && fileName != "")
            {
                Uri fileUri = new Uri(fileName);
                string localPath = fileUri.LocalPath;

                CurrentImage = new BitmapImage(fileUri);
                ImageSet.FolderPath = System.IO.Path.GetDirectoryName(localPath);

                ImageSet.ImagesInFolder = GetImagesInFolder();
                ImageSet.FoldersInFolder = GetFoldersInFolder();

                ImageSet.ItemsInFolder = new ObservableCollection<string>(ImageSet.ImagesInFolder.Concat(ImageSet.FoldersInFolder));

                SelectedItemIndex = ImageSet.ItemsInFolder.IndexOf(ImageSet.ItemsInFolder.Where(x => x.Contains(CurrentImage.UriSource.LocalPath)).FirstOrDefault());
            }
        }

        public void ChangeImage(Uri uri)
        {
            if (uri != null && SelectedItemIsImage)
            {
                if (Path.GetExtension(uri.LocalPath) != "")
                {
                    CurrentImage = new BitmapImage(uri);
                    SelectedItemIndex = ImageSet.ItemsInFolder.IndexOf(ImageSet.ItemsInFolder.Where(x => x.Contains(CurrentImage.UriSource.LocalPath)).FirstOrDefault());
                }
            }
        }

        public void NextImage()
        {
            if (ImageSet.ItemsInFolder != null)
            {
                string path = SelectedImageUri.LocalPath;
                string current = "";

                if (Directory.Exists(path))
                {
                    var directoryInfo = new DirectoryInfo(path);
                    current = directoryInfo.Name;
                }

                else if (File.Exists(path))
                {
                    current = Path.GetFileName(path); 
                }

                int index = ImageSet.ItemsInFolder.IndexOf(ImageSet.ItemsInFolder.Where(x => x.Contains(current)).FirstOrDefault());

                if (index < ImageSet.ItemsInFolder.Count - 1)
                {
                    SelectedItemIndex = index + 1;
                }
            }
        }

        public void PreviousImage()
        {
            if (ImageSet.ItemsInFolder != null)
            {
                string path = SelectedImageUri.LocalPath;
                string current = "";

                if (Directory.Exists(path))
                {
                    var directoryInfo = new DirectoryInfo(path);
                    current = directoryInfo.Name;
                }

                else if (File.Exists(path))
                {
                    current = Path.GetFileName(path);
                }

                int index = ImageSet.ItemsInFolder.IndexOf(ImageSet.ItemsInFolder.Where(x => x.Contains(current)).FirstOrDefault());

                if (index > 0)
                {
                    SelectedItemIndex = index - 1;
                }
            }

        }

        public void ChangeToRandomImage()
        {
            int index = rnd.Next(ImageSet.ItemsInFolder.Count - 1);

            ChangeImage(new Uri(ImageSet.ItemsInFolder[index]));
        }

        public void ChangeToNextFolder()
        {
            string nextFolder = _repo.GetNextFolder(this);

            string imagePath = GetImageFromFolder(nextFolder);

            SetImageSet(imagePath);
        }

        public void ChangeToPreviousFolder()
        {
            string nextFolder = _repo.GetPreviousFolder(this);

            string imagePath = GetImageFromFolder(nextFolder);

            SetImageSet(imagePath);
        }

        public void ChangeToParentFolder()
        {
            PreviousFolder = _repo.GetCurrentFolderName(this);

            string parentFolder = _repo.GetParentFolder(this);

            SetImageSetForDirectory(parentFolder);
        }

        public void ChangeToSubFolder()
        {
            if (SelectedImageUri != null) 
            {
                string imagePath = GetImageFromFolder(SelectedImageUri.LocalPath);

                if (imagePath != "")
                {
                    SetImageSet(imagePath);
                }
                else
                {
                    SetImageSetForDirectory(SelectedImageUri.LocalPath);
                }
            }
  
        }


        public string GetImageFromFolder(string folder)
        {
            string[] files = Directory.GetFiles(folder);

            string[] imageExtensions = new string[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

            foreach (string file in files)
            {

                string extension = Path.GetExtension(file).ToLower();

                foreach (string imageExtension in imageExtensions)
                {
                    if (extension == imageExtension)
                    {
                        return file;
                    }
                }
            }

            return "";
        }

        public void LoadFromState(Settings settings)
        {
            //SavePath = state.savePath;
            //SetImageSet(state.currentImage);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void LoadFullScreenWindow()
        {
            fullScreenWindow = new FullScreenWindow(this);
            fullScreenWindow.Show();
        }

        public void LoadSettingsWindow()
        {
            settingsWindow = new SettingsWindow(this);
            settingsWindow.ShowDialog();
        }

        public void CloseFullScreenWindow()
        {
            fullScreenWindow.Close();
        }

        public void LoadKeyBindingsWindow()
        {
            keyBindingsWindow = new KeyBindingsWindow();
            keyBindingsWindow.ShowDialog();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
