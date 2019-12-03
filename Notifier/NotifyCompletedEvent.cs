/*
 * Programmer(s):      Gong-Hao
 * Date:               11/14/2019
 * What the code does: Handel Notify Completed Event
 */

using System;

namespace Notifier
{
    public class NotifyCompletedEventArgs : EventArgs
    {
        public NotifyCompletedEventArgs(object userState)
        {
            UserState = userState;
        }

        public NotifyCompletedEventArgs(bool cancelled, Exception error, object userState)
        {
            Cancelled = cancelled;
            Error = error;
            UserState = userState;
        }

        public bool Cancelled { get; }
        public Exception Error { get; }
        public object UserState { get; }
    }

    public delegate void NotifyCompletedEventHandler(NotifyCompletedEventArgs e);
}
