using Presentation;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

namespace Tools
{
    public static class TaskbarShortcut
    {
        public static void UpdateTaskbarShortcut(string iconFile, string resourcePngName, string resourceIcoName)
        {
            try
            {
                if (!File.Exists(iconFile))
                    return;

                string IconFileAsIco = Path.ChangeExtension(iconFile, "ico");
                if (!File.Exists(IconFileAsIco))
                {
                    Bitmap FileBitmap = new Bitmap(iconFile, true);
                    Bitmap ResourceBitmap = new Bitmap(Application.GetResourceStream(new Uri("pack://application:,,,/Resources/" + resourcePngName)).Stream);
                    if (AreBitmapsEqual(FileBitmap, ResourceBitmap))
                    {
                        Icon ResourceIcon = new Icon(Application.GetResourceStream(new Uri("pack://application:,,,/Resources/" + resourceIcoName)).Stream);
                        using (FileStream fs = new FileStream(IconFileAsIco, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            ResourceIcon.Save(fs);
                        }
                    }
                }

                if (!File.Exists(IconFileAsIco))
                    return;

                string RoamingPath = PresentationEnvironment.UserRootFolder;
                string TaskbarShortcutPath = Path.Combine(RoamingPath, @"Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar");
                string ShortcutFileName = "PgJsonParse.lnk";
                string PgJsonParseShortcut = Path.Combine(TaskbarShortcutPath, ShortcutFileName);

                if (!File.Exists(PgJsonParseShortcut))
                    return;

                Shell32.Shell Shell = new Shell32.Shell();
                Shell32.Folder Folder = Shell.NameSpace(TaskbarShortcutPath);
                Shell32.FolderItem Item = Folder.ParseName(ShortcutFileName);
                Shell32.ShellLinkObject Link = (Shell32.ShellLinkObject)Item.GetLink;
                string OldLocation = null;
                Link.GetIconLocation(out OldLocation);
                if (string.IsNullOrEmpty(OldLocation))
                {
                    Link.SetIconLocation(IconFileAsIco, 0);
                    Link.Save();
                }
            }
            catch
            {

            }
        }

        public static bool AreBitmapsEqual(Bitmap b1, Bitmap b2)
        {
            if (b1.Size != b2.Size)
                return false;

            BitmapData bd1 = b1.LockBits(new Rectangle(new System.Drawing.Point(0, 0), b1.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            BitmapData bd2 = b2.LockBits(new Rectangle(new System.Drawing.Point(0, 0), b2.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                IntPtr bd1scan0 = bd1.Scan0;
                IntPtr bd2scan0 = bd2.Scan0;

                int stride = bd1.Stride;
                int len = stride * b1.Height;

                byte[] bd1scan0bytes = new byte[len];
                Marshal.Copy(bd1scan0, bd1scan0bytes, 0, len);
                byte[] bd2scan0bytes = new byte[len];
                Marshal.Copy(bd2scan0, bd2scan0bytes, 0, len);

                for (int i = 0; i < len; i++)
                    if (bd1scan0bytes[i] != bd2scan0bytes[i])
                        return false;

                return true;
            }
            finally
            {
                b1.UnlockBits(bd1);
                b2.UnlockBits(bd2);
            }
        }
    }
}
