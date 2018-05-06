using Microsoft.Win32;
using System.IO;
using System.Text;

namespace Presentation
{
    public class FileTools
    {
        #region Open Text File
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
    }
}
