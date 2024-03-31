using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using VSA_Viewer.Model;
using VSA_Viewer.ViewModel;

namespace VSA_Viewer.Classes
{
    public class Repo
    {
        DatabaseHandler _db;
        public Repo() 
        {
            _db = new DatabaseHandler();
        }

        public bool SaveImage(ImageSetVM VM)
        {
            string currentImage = Path.GetFileName(VM.CurrentImage.ToString());
            string currentImageFullPath = VM.CurrentImage.UriSource.LocalPath;

            try
            {
                if (Directory.Exists(VM.SavePath))
                {

                    if (!System.IO.File.Exists($"{VM.SavePath}\\{currentImage}"))
                    {
                        System.IO.File.Copy($"{currentImageFullPath}", $"{VM.SavePath}\\{currentImage}");
                        System.Windows.Forms.MessageBox.Show("Save Successful");
                        return true;
                    }
                    else
                    {
                        SaveImageRenamingLoop(currentImage, currentImageFullPath, VM.SavePath);
                        return true;
                    }
                }
                else
                {
                    using (var fbd = new FolderBrowserDialog())
                    {
                        DialogResult result = fbd.ShowDialog();

                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                        {
                            VM.SavePath = fbd.SelectedPath;
                        }
                    }
                    if (!System.IO.File.Exists($"{VM.SavePath}\\{currentImage}"))
                    {
                        System.IO.File.Copy($"{currentImageFullPath}", $"{VM.SavePath}\\{currentImage}");
                        System.Windows.Forms.MessageBox.Show("Save Successful");
                        return true;
                    }
                    else
                    {
                        SaveImageRenamingLoop(currentImage, currentImageFullPath, VM.SavePath);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("An error occurred: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _db.AddErrorLogEntry(e);
                return false;
            }

        }

        private void SaveImageRenamingLoop(string currentImage, string currentImageFullPath, string savePath)
        {
            int counter = 0;
            string extension = Path.GetExtension(currentImage);
            string fileNameNoExtension = Path.GetFileNameWithoutExtension(currentImage);
            bool runLoop = true;

            string whilePath = $"{savePath}\\{fileNameNoExtension}-{counter}{extension}";

            while (runLoop)
            {
                counter++;
                whilePath = $"{savePath}\\{fileNameNoExtension}-{counter}{extension}";

                if (!System.IO.File.Exists(whilePath))
                {
                    System.IO.File.Copy($"{currentImageFullPath}", whilePath);
                    System.Windows.Forms.MessageBox.Show("Save Successful");
                    runLoop = false;
                    break;
                }
            }
        }

        public string GetNextFolder(ImageSetVM vm)
        {
            string parentFolder = Directory.GetParent(vm.ImageSet.FolderPath).ToString();

            var allFolders = Directory.GetDirectories(parentFolder);

            var index = Array.IndexOf(allFolders, vm.ImageSet.FolderPath);

            if (index < allFolders.Length - 1)
            {
                string nextFolder = allFolders[index + 1];

                return nextFolder;
            }
            return vm.ImageSet.FolderPath;
        }

        public string GetPreviousFolder(ImageSetVM vm)
        {
            string parentFolder = Directory.GetParent(vm.ImageSet.FolderPath).ToString();

            var allFolders = Directory.GetDirectories(parentFolder);

            var index = Array.IndexOf(allFolders, vm.ImageSet.FolderPath);

            if (index > 0)
            {
                string nextFolder = allFolders[index - 1];

                return nextFolder;
            }
            return vm.ImageSet.FolderPath;
        }

        public string GetParentFolder(ImageSetVM vm)
        {
            if (Directory.GetParent(vm.ImageSet.FolderPath) != null)
            {
                string parentFolder = Directory.GetParent(vm.ImageSet.FolderPath).ToString();

                if (parentFolder != null)

                    return parentFolder;

            }

            return vm.ImageSet.FolderPath;
        }

        public string GetCurrentFolderName(ImageSetVM vm)
        {
            var directoryInfo = new DirectoryInfo(vm.ImageSet.FolderPath);
            if (directoryInfo.Exists)
            {
                return directoryInfo.Name;
            }
            else
            {
                return vm.ImageSet.FolderPath;
            }
        }

        public bool SaveEntireFolder(ImageSetVM vm)
        {
            try
            {
                string sourceDirectory = System.IO.Path.GetDirectoryName(vm.SelectedImageUri.LocalPath);
                string destinationDirectory = vm.SavePath;
                string targetPath = "";

                bool CreateDirectory()
                {
                    try
                    {
                        string dir = vm.SavePath;
                        string folderName = System.IO.Directory.GetParent(vm.SelectedImageUri.LocalPath).Name;
                        string target = $"{dir}\\{folderName}";
                        int counter = 2;

                        while (Directory.Exists(target))
                        {
                            target = $"{target} {counter}";
                            counter++;
                        }

                        targetPath = target;
                        System.IO.Directory.CreateDirectory(target);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }

                void CopyAll(string source, string target)
                {
                    System.IO.Directory.CreateDirectory(target);

                    foreach (string directoryPath in System.IO.Directory.GetDirectories(source, "*", System.IO.SearchOption.AllDirectories))
                        System.IO.Directory.CreateDirectory(directoryPath.Replace(source, target));

                    foreach (string filePath in System.IO.Directory.GetFiles(source, "*.*", System.IO.SearchOption.AllDirectories))
                        System.IO.File.Copy(filePath, filePath.Replace(source, target), true);
                }

                bool createDirectorySuccess = CreateDirectory();
                if (!createDirectorySuccess)
                {
                    throw new Exception("Failed to create the directory.");
                }

                if (System.IO.Directory.Exists(sourceDirectory) && System.IO.Directory.Exists(targetPath))
                {
                    CopyAll(sourceDirectory, targetPath);
                    System.Windows.Forms.MessageBox.Show("Save Successful");
                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show($"Source or target directory does not exist: {sourceDirectory}");
                    return false;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("An error occurred: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _db.AddErrorLogEntry(e);
                return false;
            }

        }

    }
}
