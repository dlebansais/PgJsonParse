#if CSHTML5
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace Presentation
{
    public class FileTools
    {
        #region File Dialog
        public static bool OpenTextFile(out string fileName, out string content)
        {
            fileName = null;
            content = null;
            return false;
        }

        /*
        private void OnFileOpened(object sender, CSHTML5.Extensions.FileOpenDialog.FileOpenedEventArgs e)
        {
            MessageBox.Show(e.Text);
            using (StringReader sr = new StringReader(e.Text))
            {
                string Cotnent = sr.ReadToEnd();
            }
        }*/

        public static void SaveTextFile(string fileName, string content)
        {
            //Task task = FileSaveDialog.SaveTextToFile(fileName, content);
            //task.Wait();
        }
        #endregion

        #region File
        public static bool FileExists(string fileName)
        {
            return Bookkeeping.FileExists(fileName);
        }

        public static string LoadTextFile(string fileName, FileMode mode)
        {
            byte[] ZipBytes = LoadFromIsolatedStorageFile(ToISFileName(fileName), mode);
            if (ZipBytes == null)
                return null;

            try
            {
                int InflatedLength = BitConverter.ToInt32(ZipBytes, 0);
                MemoryStream inflatedStream = new MemoryStream();
                inflatedStream.Write(ZipBytes, sizeof(Int32), ZipBytes.Length - sizeof(Int32));
                inflatedStream.Seek(0, SeekOrigin.Begin);
                InflaterInputStream iis = new InflaterInputStream(inflatedStream);
                byte[] Bytes = new byte[InflatedLength];

                int ReadCount = iis.Read(Bytes, 0, InflatedLength);

                Encoding encoding = new UTF8Encoding();
                string Content = encoding.GetString(Bytes, 0, InflatedLength);

                //Debug.WriteLine($"Loaded text content (size: {Content.Length})");

                return Content;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                return null;
            }
        }

        public static bool CommitTextFile(string fileName, string content)
        {
            Encoding encoding = new UTF8Encoding();
            byte[] Bytes = (content == null) ? null : encoding.GetBytes(content);
            byte[] ZipBytes;

            try
            {
                byte[] CountBytes = BitConverter.GetBytes(Bytes.Length);
                MemoryStream deflatedStream = new MemoryStream();
                DeflaterOutputStream dos = new DeflaterOutputStream(deflatedStream);
                dos.Write(Bytes, 0, Bytes.Length);
                dos.Finish();
                int DeflatedLength = (int)deflatedStream.Length;
                deflatedStream.Seek(0, SeekOrigin.Begin);
                ZipBytes = new byte[sizeof(Int32) + DeflatedLength];
                Array.Copy(CountBytes, 0, ZipBytes, 0, sizeof(Int32));
                deflatedStream.Read(ZipBytes, sizeof(Int32), DeflatedLength);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                return false;
            }

            bool Success = SaveToIsolatedStorageFile(ToISFileName(fileName), ZipBytes);
            if (Success)
                Bookkeeping.CreateFile(fileName);

            return Success;
        }

        public static byte[] LoadBinaryFile(string fileName, FileMode mode)
        {
            return LoadFromIsolatedStorageFile(ToISFileName(fileName), mode);
        }

        public static bool CommitBinaryFile(string fileName, byte[] content)
        {
            bool Success = SaveToIsolatedStorageFile(ToISFileName(fileName), content);
            if (Success)
                Bookkeeping.CreateFile(fileName);

            return Success;
        }

        public static void CopyFile(string sourceFileName, string destinationFileName)
        {
            Bookkeeping.CreateFile(destinationFileName);

            string Source = ToISFileName(sourceFileName);
            string Destination = ToISFileName(destinationFileName);

            if (IsolatedStorageFileExists(Source))
                CopyIsolatedStorageFile(Source, Destination, out bool success);
        }
        #endregion

        #region Directory
        public static bool DirectoryExists(string directoryName)
        {
            return Bookkeeping.DirectoryExists(directoryName);
        }

        public static void CreateDirectory(string directoryName)
        {
            Bookkeeping.CreateDirectory(directoryName);
        }

        public static void DeleteDirectory(string directoryName)
        {
            Bookkeeping.DeleteDirectory(directoryName);
        }

        public static string[] DirectoryFolders(string directoryName)
        {
            return Bookkeeping.DirectoryFolders(directoryName);
        }

        public static string[] DirectoryFiles(string directoryName, string extension)
        {
            return Bookkeeping.DirectoryFiles(directoryName, extension);
        }
        #endregion

        #region Bitmap
        public static FileBitmapImage LoadBitmap(string fileName)
        {
            string Source = ToISFileName(fileName);
            if (!IsolatedStorageFileExists(Source))
                return null;

            if (BitmapCache.ContainsKey(Source))
                return BitmapCache[Source];
            else
            {
                FileBitmapImage Result = LoadFromIsolatedStorageBitmap(Source);
                if (Result != null)
                    BitmapCache.Add(Source, Result);

                return Result;
            }
        }

        private static Dictionary<string, FileBitmapImage> BitmapCache = new Dictionary<string, FileBitmapImage>();
        #endregion

        #region Isolated Storage
        private static string ToISFileName(string fileName)
        {
            fileName = fileName.Replace('\\', '_');
            fileName = fileName.Replace('/', '_');

            return fileName;
        }

        private static string ToISDirectoryName(string directoryName)
        {
            if (directoryName.EndsWith("/") || directoryName.EndsWith("\\"))
                return ToISFileName(directoryName);
            else
                return ToISFileName(directoryName + "/");
        }

        private static IsolatedStorageFile GetStorageRoot()
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();
            return storage;
        }

        private static bool IsolatedStorageFileExists(string fileName)
        {
            try
            {
                using (IsolatedStorageFile storage = GetStorageRoot())
                {
                    return storage.FileExists(fileName);
                }
            }
            catch (Exception e)
            {
                PrintException(e, "File Exists", fileName);
            }

            return false;
        }

        private static byte[] LoadFromIsolatedStorageFile(string fileName, FileMode mode)
        {
            byte[] Content = null;

            try
            {
                using (IsolatedStorageFile storage = GetStorageRoot())
                {
                    using (IsolatedStorageFileStream fs = storage.OpenFile(fileName, mode))
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            Content = br.ReadBytes((int)fs.Length);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                PrintException(e, "Load File", fileName);
            }

            if (Content.Length == 0)
                return null;
            else
                return Content;
        }

        private static bool SaveToIsolatedStorageFile(string fileName, byte[] content)
        {
            //Debug.WriteLine($"Writing {content.Length} bytes in file {fileName}");

            try
            {
                using (IsolatedStorageFile storage = GetStorageRoot())
                {
                    using (IsolatedStorageFileStream fs = storage.CreateFile(fileName))
                    {
                        try
                        {
                            fs.Write(content, 0, content.Length);
                            fs.Close();
                            return true;
                        }
                        catch (Exception e)
                        {
                            PrintException(e, "Write File Data", fileName);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                PrintException(e, "Save File", fileName);
            }

            return false;
        }

        private static void DeleteIsolatedStorageFile(string fileName)
        {
            try
            {
                using (IsolatedStorageFile storage = GetStorageRoot())
                {
                    using (IsolatedStorageFileStream fs = storage.CreateFile(fileName))
                    {
                        try
                        {
                            byte[] content = new byte[0];
                            fs.Write(content, 0, 0);
                            fs.Close();
                        }
                        catch (Exception e)
                        {
                            PrintException(e, "Write File Data", fileName);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                PrintException(e, "Delete File", fileName);
            }
        }

        private static void CopyIsolatedStorageFile(string sourceFileName, string destinationFileName, out bool success)
        {
            success = false;

            try
            {
                using (IsolatedStorageFile storage = GetStorageRoot())
                {
                    using (IsolatedStorageFileStream src = storage.OpenFile(sourceFileName, FileMode.Open))
                    {
                        using (BinaryReader br = new BinaryReader(src))
                        {
                            byte[] Content = br.ReadBytes((int)src.Length);

                            using (IsolatedStorageFileStream dst = storage.CreateFile(destinationFileName))
                            {
                                dst.Write(Content, 0, Content.Length);
                                dst.Close();

                                success = true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                PrintException(e, "Copy File", sourceFileName + "/" + destinationFileName);
            }
        }

        private static FileBitmapImage LoadFromIsolatedStorageBitmap(string fileName)
        {
            FileBitmapImage Result = null;

            try
            {
                IsolatedStorageFile storage = GetStorageRoot();
                IsolatedStorageFileStream fs = storage.OpenFile(fileName, FileMode.Open);
                Result = new FileBitmapImage(storage, fs);
            }
            catch (Exception e)
            {
                PrintException(e, "Load Bitmap", fileName);
            }

            return Result;
        }

        private static void PrintException(Exception e, string Operation, string fileName)
        {
            Debug.WriteLine("Isolated Storage Error, Operation=" + Operation + ", Filename=" + fileName + ": " + e.Message);
        }
        #endregion
    }
}
#else
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

        public static void DeleteFile(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
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
#endif