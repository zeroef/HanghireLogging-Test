using Hangfire.Common;
using Hangfire.Server;
using System;

namespace HangfireLogging
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface)]
    public class LogToConsoleAttribute : JobFilterAttribute, IServerFilter
    {
        private IDisposable _subscription;

        public void OnPerforming(PerformingContext filterContext)
        {
            _subscription = HangfireConsoleLogger.InContext(filterContext);
        }
        public void OnPerformed(PerformedContext filterContext)
        {
            _subscription?.Dispose();
        }
    }
}
