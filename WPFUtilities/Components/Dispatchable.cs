using System;
using System.Windows;
using System.Windows.Threading;

using Common.Logger;

using WPFUtilities.Controls;

namespace WPFUtilities.Components
{
    public class Dispatchable
    {
        protected Dispatcher Dispatcher;

        public Dispatchable()
        {
            if (Application.Current != null)
                Dispatcher = Application.Current.Dispatcher;
        }

        public void BeginInvoke(
            Action a,
            DispatcherPriority p = DispatcherPriority.Normal
            )
        {
            this.Dispatcher.BeginInvoke(a, p);
        }

        public void InvokeWhenUIReady(
            Action a,
            DispatcherPriority p = DispatcherPriority.Background
            )
        {
            this.Dispatcher.BeginInvoke(a, p);
        }

        public void Invoke(
            Action a,
            DispatcherPriority p = DispatcherPriority.Normal
            )
        {
            this.Dispatcher.Invoke(a, p);
        }

        protected void Tryc(
            Action action,
            Action onErrorAction = null)
        {
            try
            {
                action();
            }
            catch (Exception Ex)
            {
                BeginInvoke(() =>
                {
                    CurrentApp.IsBuzy = false;
                    onErrorAction?.Invoke();
                    var s = "";
                    while (Ex != null)
                    {
                        if (!string.IsNullOrWhiteSpace(s))
                            s += "\r\n";
                        s += Ex.Message;
                        Ex = Ex.InnerException;
                    }
                    Log.Error(s);
                });
            }
        }

        protected IApp CurrentApp
        {
            get
            {
                return Application.Current as IApp;
            }
        }

    }
}