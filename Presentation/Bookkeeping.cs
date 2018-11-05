#if CSHTML5
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Presentation
{
    public static class Bookkeeping
    {
        private static readonly string BookkeepingFileName = "book.keeping";

        public static void Update()
        {
            if (Root != null)
                return;

            Root = new Dictionary<string, IDictionary>();

            string Book = FileTools.LoadTextFile(BookkeepingFileName, FileMode.OpenOrCreate);
            if (Book == null)
                Book = "";

            string[] Lines = Book.Split('\n');
            foreach (string Line in Lines)
            {
                if (string.IsNullOrEmpty(Line))
                    continue;

                if (Line.EndsWith("/"))
                {
                    IList<string> DirectoryPath = FromDirectoryName(Line);
                    InsertDirectory(DirectoryPath, out bool inserted);
                }
                else
                {
                    IList<string> FilePath = FromFileName(Line);
                    InsertFile(FilePath, out bool inserted);
                }
            }
        }

        public static void Commit()
        {
            if (Root == null)
                Root = new Dictionary<string, IDictionary>();

            string Lines = "";
            string Directory = "";
            Commit(ref Lines, Root, Directory);

            FileTools.CommitTextFile(BookkeepingFileName, Lines);
        }

        public static void Commit(ref string Lines, Dictionary<string, IDictionary> Current, string Directory)
        {
            foreach (KeyValuePair<string, IDictionary> Entry in Current)
            {
                Dictionary<string, IDictionary> Folder = Entry.Value as Dictionary<string, IDictionary>;
                if (Folder == null)
                    Lines += Directory + Entry.Key + "\n";
                else
                {
                    string NewDirectory = Directory + Entry.Key + "/";
                    Lines += NewDirectory + "\n";
                    Commit(ref Lines, Folder, NewDirectory);
                }
            }
        }

        public static IList<string> FromFileName(string fileName)
        {
            string path = fileName;
            path = path.Replace("\\", "/");

            string[] components = path.Split('/');

            List<string> Result = new List<string>();

            foreach (string component in components)
                if (string.IsNullOrEmpty(component))
                    return null;
                else
                    Result.Add(component);

            return Result;
        }

        public static string ToFileName(IList<string> filePath)
        {
            string Result = "";

            foreach (string component in filePath)
            {
                if (Result.Length > 0)
                    Result += "\\";

                Result += component;
            }

            return Result;
        }

        public static IList<string> FromDirectoryName(string directoryName)
        {
            string path = directoryName;
            path = path.Replace("\\", "/");
            if (path.EndsWith("/"))
                path = path.Substring(0, path.Length - 1);

            string[] components = path.Split('/');

            List<string> Result = new List<string>();

            foreach (string component in components)
                if (string.IsNullOrEmpty(component))
                    return null;
                else
                    Result.Add(component);

            return Result;
        }

        public static string ToDirectoryName(IList<string> directoryPath)
        {
            string Result = "";

            foreach (string component in directoryPath)
            {
                if (Result.Length > 0)
                    Result += "\\";

                Result += component;
            }

            return Result;
        }

        public static bool DirectoryExists(string directoryName)
        {
            Update();

            IList<string> DirectoryPath = FromDirectoryName(directoryName);
            Dictionary<string, IDictionary> Current = Root;

            foreach (string Component in DirectoryPath)
                if (Current == null || !Current.ContainsKey(Component))
                    return false;
                else
                    Current = Current[Component] as Dictionary<string, IDictionary>;

            if (Current == null)
                return false;

            return true;
        }

        public static bool FileExists(string fileName)
        {
            Update();

            IList<string> FilePath = FromFileName(fileName);
            Dictionary<string, IDictionary> Current = Root;

            foreach (string Component in FilePath)
                if (Current == null || !Current.ContainsKey(Component))
                    return false;
                else
                    Current = Current[Component] as Dictionary<string, IDictionary>;

            if (Current != null)
                return false;

            return true;
        }

        public static void CreateDirectory(string directoryName)
        {
            Update();

            IList<string> DirectoryPath = FromDirectoryName(directoryName);
            InsertDirectory(DirectoryPath, out bool inserted);

            if (inserted)
                Commit();
        }

        private static void InsertDirectory(IList<string> directoryPath, out bool inserted)
        {
            Dictionary<string, IDictionary> Current = Root;

            int i;
            for (i = 0; i < directoryPath.Count; i++)
            {
                string Component = directoryPath[i];

                if (Current == null)
                    throw new InvalidOperationException();
                else if (Current.ContainsKey(Component))
                    Current = Current[Component] as Dictionary<string, IDictionary>;
                else
                    break;
            }

            if (Current == null)
                throw new InvalidOperationException();

            if (i == directoryPath.Count)
                inserted = false;
            else
            {
                for (; i < directoryPath.Count; i++)
                {
                    string Component = directoryPath[i];
                    Current.Add(Component, new Dictionary<string, IDictionary>());
                    Current = Current[Component] as Dictionary<string, IDictionary>;
                }

                inserted = true;
            }
        }

        public static void CreateFile(string fileName)
        {
            if (fileName == BookkeepingFileName)
                return;

            Update();

            IList<string> FilePath = FromFileName(fileName);
            InsertFile(FilePath, out bool inserted);

            if (inserted)
                Commit();
        }

        public static void InsertFile(IList<string> filePath, out bool inserted)
        {
            Dictionary<string, IDictionary> Current = Root;

            int i;
            for (i = 0; i + 1 < filePath.Count; i++)
            {
                string Component = filePath[i];

                if (Current == null)
                    throw new InvalidOperationException();
                else if (Current.ContainsKey(Component))
                    Current = Current[Component] as Dictionary<string, IDictionary>;
                else
                    break;
            }

            for (; i + 1 < filePath.Count; i++)
            {
                string Component = filePath[i];
                Current.Add(Component, new Dictionary<string, IDictionary>());
                Current = Current[Component] as Dictionary<string, IDictionary>;
            }

            string FileName = filePath[i];

            if (Current.ContainsKey(FileName))
                if (Current[FileName] == null)
                {
                    inserted = false;
                    return;
                }
                else
                    throw new InvalidOperationException();

            Current.Add(FileName, null);
            inserted = true;
        }

        public static void DeleteDirectory(string directoryName)
        {
            Update();

            IList<string> DirectoryPath = FromDirectoryName(directoryName);
            Dictionary<string, IDictionary> Current = Root;

            int i;
            for (i = 0; i + 1 < DirectoryPath.Count; i++)
            {
                string Component = DirectoryPath[i];

                if (Current == null)
                    throw new InvalidOperationException();
                else if (Current.ContainsKey(Component))
                    Current = Current[Component] as Dictionary<string, IDictionary>;
                else
                    break;
            }

            if (i + 1 == DirectoryPath.Count)
            {
                string Component = DirectoryPath[i];
                Current.Remove(Component);

                Commit();
            }
        }

        public static string[] DirectoryFolders(string directoryName)
        {
            Update();

            IList<string> DirectoryPath = FromDirectoryName(directoryName);
            Dictionary<string, IDictionary> Current = Root;
            string Directory = "";

            int i;
            for (i = 0; i < DirectoryPath.Count; i++)
            {
                string Component = DirectoryPath[i];

                if (Current == null)
                    throw new InvalidOperationException();
                else if (Current.ContainsKey(Component))
                {
                    Directory += Component + "/";
                    Current = Current[Component] as Dictionary<string, IDictionary>;
                }
                else
                    return new string[0];
            }

            int FolderCount = 0;
            foreach (KeyValuePair<string, IDictionary> Entry in Current)
                if (Entry.Value != null)
                    FolderCount++;

            string[] Result = new string[FolderCount];

            int FolderIndex = 0;
            foreach (KeyValuePair<string, IDictionary> Entry in Current)
                if (Entry.Value != null)
                    Result[FolderIndex++] = Directory + Entry.Key;

            return Result;
        }

        public static string[] DirectoryFiles(string directoryName, string extension)
        {
            Update();

            IList<string> DirectoryPath = FromDirectoryName(directoryName);
            Dictionary<string, IDictionary> Current = Root;
            string Directory = "";

            int i;
            for (i = 0; i < DirectoryPath.Count; i++)
            {
                string Component = DirectoryPath[i];

                if (Current == null)
                    throw new InvalidOperationException();
                else if (Current.ContainsKey(Component))
                {
                    Directory += Component + "/";
                    Current = Current[Component] as Dictionary<string, IDictionary>;
                }
                else
                    return new string[0];
            }

            int FileCount = 0;
            foreach (KeyValuePair<string, IDictionary> Entry in Current)
                if (Entry.Value == null && IsExtensionMatch(Entry.Key, extension))
                    FileCount++;

            string[] Result = new string[FileCount];

            int FileIndex = 0;
            foreach (KeyValuePair<string, IDictionary> Entry in Current)
                if (Entry.Value == null && IsExtensionMatch(Entry.Key, extension))
                    Result[FileIndex++] = Directory + Entry.Key;

            return Result;
        }

        private static bool IsExtensionMatch(string name, string extension)
        {
            return name.EndsWith("." + extension);
        }

        private static Dictionary<string, IDictionary> Root = null;
    }
}
#endif
