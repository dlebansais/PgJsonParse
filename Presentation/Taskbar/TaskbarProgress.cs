#if CSHTML5
#else
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Presentation
{
    public static class TaskbarProgress
    {
        [ComImport()]
        [Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface ITaskbarList3
        {
            // ITaskbarList
            [PreserveSig]
            void HrInit();
            [PreserveSig]
            void AddTab(IntPtr hwnd);
            [PreserveSig]
            void DeleteTab(IntPtr hwnd);
            [PreserveSig]
            void ActivateTab(IntPtr hwnd);
            [PreserveSig]
            void SetActiveAlt(IntPtr hwnd);

            // ITaskbarList2
            [PreserveSig]
            void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

            // ITaskbarList3
            [PreserveSig]
            int SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);
            [PreserveSig]
            int SetProgressState(IntPtr hwnd, TaskbarState state);
        }

        [ComImport()]
        [Guid("56fdf344-fd6d-11d0-958a-006097c9a090")]
        [ClassInterface(ClassInterfaceType.None)]
        private class TaskbarInstance
        {
        }

        private static readonly ITaskbarList3 TaskbarInstanceSingleton = (ITaskbarList3)new TaskbarInstance();
        private static readonly bool IsTaskbarSupported = Environment.OSVersion.Version >= new Version(6, 1);

        public static void SetState(Window window, TaskbarState taskbarState)
        {
            if (IsTaskbarSupported)
            {
                IntPtr WindowHandle = new WindowInteropHelper(window).Handle;
                var Result = TaskbarInstanceSingleton.SetProgressState(WindowHandle, taskbarState);
            }
        }

        public static void SetValue(Window window, double progressValue, double progressMax)
        {
            if (IsTaskbarSupported)
            {
                IntPtr WindowHandle = new WindowInteropHelper(window).Handle;
                var Result = TaskbarInstanceSingleton.SetProgressValue(WindowHandle, (ulong)progressValue, (ulong)progressMax);
            }
        }
    }
}
#endif