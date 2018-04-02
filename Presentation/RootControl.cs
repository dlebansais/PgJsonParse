using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Presentation
{
    public class RootControl : Window
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

        protected virtual void InvokeAndForget(Action action)
        {
            Dispatcher.InvokeAsync(action);
        }

        protected virtual async Task InvokeAndWait(Action action)
        {
            Task CompleteTask = new Task(new Action(() => { }));
            await Dispatcher.InvokeAsync(new Action(() => ExecuteAction(action, CompleteTask)));
            await CompleteTask;
        }

        private void ExecuteAction(Action action, Task CompleteTask)
        {
            action();
            CompleteTask.RunSynchronously();
        }

        protected virtual async Task<TResult> RunAsync<TResult>(Func<TResult> function)
        {
            TResult Result = await Task.Run(function);
            return Result;
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
