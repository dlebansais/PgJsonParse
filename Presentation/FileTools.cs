using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Presentation
{
    public class FileTools
    {
        #region File Dialog
        public static bool OpenTextFile(ref string fileName, out string content)
        {
            OpenFileDialog Dlg = new OpenFileDialog();
            Dlg.FileName = fileName;
            Dlg.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            bool? Result = Dlg.ShowDialog();

            if (Result.HasValue && Result.Value == true)
            {
                using (FileStream fs = new FileStream(Dlg.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.ASCII))
                    {
                        fileName = Dlg.FileName;
                        content = sr.ReadToEnd();
                        return true;
                    }
                }
            }

            fileName = null;
            content = null;
            return false;
        }

        public static bool SaveTextFile(ref string fileName, string content)
        {
            SaveFileDialog Dlg = new SaveFileDialog();
            Dlg.FileName = fileName;
            Dlg.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            bool? Result = Dlg.ShowDialog();

            if (Result.HasValue && Result.Value == true)
            {
                using (FileStream fs = new FileStream(Dlg.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.ASCII))
                    {
                        sw.Write(content);
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

        #region File
        public static bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        public static string LoadTextFile(string fileName, FileMode mode)
        {
            using (FileStream fs = new FileStream(fileName, mode, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string Result = sr.ReadToEnd();
                    return Result;
                }
            }
        }

        public static bool CommitTextFile(string fileName, string content)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.ASCII))
                {
                    sw.Write(content);
                    return true;
                }
            }
        }

        public static byte[] LoadBinaryFile(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] Result = br.ReadBytes((int)fs.Length);
                    return Result;
                }
            }
        }

        public static bool CommitBinaryFile(string fileName, byte[] content)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(content);
                    return true;
                }
            }
        }

        public static void CopyFile(string sourceFileName, string destinationFileName)
        {
            if (File.Exists(sourceFileName))
                File.Copy(sourceFileName, destinationFileName, true);
        }
        #endregion

        #region Directory
        public static bool DirectoryExists(string directoryName)
        {
            return Directory.Exists(directoryName);
        }

        public static void CreateDirectory(string directoryName)
        {
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
        }

        public static void DeleteDirectory(string directoryName)
        {
            if (Directory.Exists(directoryName))
                Directory.Delete(directoryName, true);
        }

        public static string[] DirectoryFolders(string directoryName)
        {
            if (!Directory.Exists(directoryName))
                return new string[0];

            return Directory.GetDirectories(directoryName);
        }

        public static string[] DirectoryFiles(string directoryName, string searchPattern)
        {
            searchPattern = "*." + searchPattern;

            if (!Directory.Exists(directoryName))
                return new string[0];

            return Directory.GetFiles(directoryName, searchPattern);
        }
        #endregion

        #region Bitmap
        public static FileBitmapImage LoadBitmap(string fileName)
        {
            return new FileBitmapImage(fileName);
        }
        #endregion
    }
}
