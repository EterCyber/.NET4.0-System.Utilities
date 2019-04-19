namespace System.Utilities.Common
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public static class ThreadHelper
    {
        public static void CancelSleep(this Thread thread)
        {
            if (thread.ThreadState == ThreadState.WaitSleepJoin)
            {
                thread.Interrupt();
            }
        }

        public static void StartAndIgnoreAbort(this Thread thread, Action<Exception> failAction)
        {
            try
            {
                thread.Start();
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception exception)
            {
                if (failAction != null)
                {
                    failAction(exception);
                }
            }
        }
    }
}

