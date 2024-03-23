using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VSA_Viewer.Model;
using VSA_Viewer.ViewModel;

namespace VSA_Viewer.Classes
{
    public static class Repo
    {
        public static void SaveImage(ImageSetVM VM)
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
                        MessageBox.Show("Save Successful");
                    }
                    else
                    {
                        SaveImageRenamingLoop(currentImage, currentImageFullPath, VM.SavePath);
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
                        MessageBox.Show("Save Successful");
                    }
                    else
                    {
                        SaveImageRenamingLoop(currentImage, currentImageFullPath, VM.SavePath);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong");
            }

        }

        private static void SaveImageRenamingLoop(string currentImage, string currentImageFullPath, string savePath)
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
                    MessageBox.Show("Save Successful");
                    runLoop = false;
                    break;
                }
            }
        }

        public static string GetNextFolder(ImageSetVM vm)
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

        public static string GetPreviousFolder(ImageSetVM vm)
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

        public static string GetParentFolder(ImageSetVM vm)
        {
            if (Directory.GetParent(vm.ImageSet.FolderPath) != null)
            {
                string parentFolder = Directory.GetParent(vm.ImageSet.FolderPath).ToString();

                if (parentFolder != null)

                    return parentFolder;

            }

            return vm.ImageSet.FolderPath;
        }

        public static string GetCurrentFolderName(ImageSetVM vm)
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
    }
}
