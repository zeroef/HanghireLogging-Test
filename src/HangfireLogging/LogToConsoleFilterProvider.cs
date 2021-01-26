using Hangfire.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangfireLogging
{
    public class LogToConsoleFilterProvider : IJobFilterProvider
    {
        public IEnumerable<JobFilter> GetFilters(Job job)
        {
            yield return new JobFilter(new LogToConsoleAttribute(), JobFilterScope.Global, 0);
        }
    }
}
