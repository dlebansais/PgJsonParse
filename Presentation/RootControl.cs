#if CSHTML5
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Presentation
{
    public class RootControl : Page, IDispatcherSource
    {
        public RootControl(RootControlMode mode)
        {
            Mode = mode;

            switch (Mode)
            {
                case RootControlMode.ResizedWithCaption:
                    break;

                case RootControlMode.CustomShape:
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }

        public RootControlMode Mode { get; set; }
        public bool IsControlVisible { get; private set; }
        public object ActionDispatcher { get { return Dispatcher; } }
        public string Title { get; set; }

        public virtual void ControlClose()
        {
        }

        protected virtual void OnControlClose()
        {
        }

        protected virtual void OnControlClosing(ref bool Cancel)
        {
        }

        protected virtual void OnControlClosed()
        {
        }

        public void StartTask(Func<bool> action, Action<bool> callback)
        {
            TimerAction = action;
            TimerCallback = callback;

            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Tick += OnTimerStarted;
            Timer.Interval = TimeSpan.FromMilliseconds(500);

            Debug.WriteLine("Task scheduled");
            Timer.Start();
        }

        Func<bool> TimerAction;
        Action<bool> TimerCallback;

        private void OnTimerStarted(object sender, object e)
        {
            Debug.WriteLine("Task started");

            DispatcherTimer Timer = sender as DispatcherTimer;
            if (Timer.IsEnabled)
                Timer.Stop();

            Func<bool> action = TimerAction;
            Action<bool> callback = TimerCallback;
            TimerAction = null;
            TimerCallback = null;

            if (action != null && callback != null)
            {
                bool Result = action();
                Dispatcher.BeginInvoke(() => callback(Result));
                Debug.WriteLine("Task completed");
            }
        }

        public virtual void SwitchTo(RootControl ctrl, Action action)
        {
            //ctrl.Show();
            Window.Current.Content = ctrl;

            ctrl.IsControlVisible = true;
            IsControlVisible = false;
            ControlClose();
            action();
        }

        public virtual void NavigateTo(string address)
        {
            //Process.Start(address);
        }

        public void SubscribeToCommand(string resourceName, EventHandler handler)
        {
            ApplicationCommand.SubscribeToControlCommand(this, resourceName, handler);
        }

        protected object GetResourceByName(string name)
        {
            return Resources[name];
        }

        #region Progress
        public void SetTaskbarState(TaskbarStates taskbarState)
        {
        }

        public void SetTaskbarProgressValue(double progressValue, double progressMax)
        {
        }
        #endregion
    }
}
#else
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Presentation
{
    public class RootControl : Window, IDispatcherSource
    {
        public RootControl(RootControlMode mode)
        {
            Mode = mode;

            switch (Mode)
            {
                case RootControlMode.ResizedWithCaption:
                    break;

                case RootControlMode.CustomShape:
                    Background = Brushes.Transparent;
                    WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    ResizeMode = ResizeMode.NoResize;
                    SizeToContent = SizeToContent.WidthAndHeight;
                    WindowStyle = WindowStyle.None;
                    AllowsTransparency = true;
                    break;

                default:
                    throw new System.InvalidOperationException();
            }

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, OnControlClose));
        }

        public RootControlMode Mode { get; set; }
        public bool IsControlVisible { get { return IsVisible; } }
        public object ActionDispatcher { get { return Dispatcher; } }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (Mode == RootControlMode.CustomShape)
                DragMove();
            else
                base.OnMouseLeftButtonDown(e);
        }

        public virtual void ControlClose()
        {
            Close();
        }

        protected virtual void OnControlClose(object sender, EventArgs e)
        {
            OnControlClose();
        }

        protected virtual void OnControlClose()
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            bool Cancel = e.Cancel;
            OnControlClosing(ref Cancel);
            e.Cancel = Cancel;
        }

        protected virtual void OnControlClosing(ref bool Cancel)
        {
        }

        protected override void OnClosed(EventArgs e)
        {
            OnControlClosed();
            base.OnClosed(e);
        }

        protected virtual void OnControlClosed()
        {
        }

        public void StartTask(Action action)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, action);
        }

        public void StartTask(Func<bool> action, Action<bool> callback)
        {
            Task ParseTask = new Task(() => ExecuteTask(action, callback));
            ParseTask.Start();
        }

        private void ExecuteTask(Func<bool> action, Action<bool> callback)
        {
            bool Result = action();
            Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, callback, Result);
        }

        public virtual void SwitchTo(RootControl ctrl, Action action)
        {
            ctrl.Show();
            ControlClose();
            action();
        }

        public virtual void NavigateTo(string address)
        {
            Process.Start(address);
        }

        public void SubscribeToCommand(string resourceName, EventHandler handler)
        {
            ApplicationCommand.SubscribeToControlCommand(this, resourceName, handler);
        }

        protected object GetResourceByName(string name)
        {
            return Resources[name];
        }

#region Progress
        public void SetTaskbarState(TaskbarStates taskbarState)
        {
            Dispatcher.BeginInvoke(new Action(() => OnSetState(this, taskbarState)));
        }

        public static void OnSetState(Window window, TaskbarStates taskbarState)
        {
            TaskbarProgress.SetState(window, taskbarState);
        }

        public void SetTaskbarProgressValue(double progressValue, double progressMax)
        {
            Dispatcher.BeginInvoke(new Action(() => OnSetValue(this, progressValue, progressMax)));
        }

        public static void OnSetValue(Window window, double progressValue, double progressMax)
        {
            TaskbarProgress.SetValue(window, progressValue, progressMax);
        }
#endregion
    }
}
#endif
