using Microsoft.Win32;
using System;
using System.IO;
using System.Text;

namespace Presentation
{
    public class FileTools
    {
        #region File Dialog
        public static bool OpenTextFile(out string fileName, out string content)
        {
            OpenFileDialog Dlg = new OpenFileDialog();
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

        public static void SaveTextFile(string fileName, string content)
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
                    }
                }
            }
        }
        #endregion

        #region File
        public static bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        public static string LoadTextFile(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string Result = sr.ReadToEnd();
                    return Result;
                }
            }
        }

        public static void CommitTextFile(string fileName, string content)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.ASCII))
                {
                    sw.Write(content);
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

        public static void CommitBinaryFile(string fileName, byte[] content)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(content);
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
        #endregion

        #region Uri
        public static Uri GetFileUri(string fileName)
        {
            Uri uri = new Uri(fileName);
            return uri;
        }
        #endregion
    }
}
