using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Utilities
{
    public class RetryAPICall
    {
        public static void Do(Action action, IRetryAPICall retryAPICall, TimeSpan retryInterval, int maxAttemptCount = 3)
        {
            Do<object>(() =>
            {
                action();
                return null;
            }, retryAPICall, retryInterval, maxAttemptCount);
        }

        public static T Do<T>(Func<T> action, IRetryAPICall retryAPICall, TimeSpan retryInterval, int maxAttemptCount = 3)
        {

            // list of returned exceptions
            var exceptions = new List<Exception>();

            for (int attempted = 0; attempted < maxAttemptCount; attempted++)
            {
                try
                {
                    if (attempted > 0)
                    {
                        Thread.Sleep(retryInterval);
                    }
                    return action();
                }
                catch (Exception ex)
                {
                    bool IsAllowedForRetry = retryAPICall.IsAllowRetry(ex);
                    if (IsAllowedForRetry)
                    {
                        exceptions.Add(ex);
                    }
                    else
                    {
                        exceptions.Add(ex);
                        break;
                    }
                }
            }
            throw new AggregateException(exceptions);
        }
    }
}
