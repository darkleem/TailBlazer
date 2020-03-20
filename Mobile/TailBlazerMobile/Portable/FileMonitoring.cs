using System;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using DynamicData.Annotations;
using DynamicData.Kernel;

namespace TailBlazerMobile.Portable
{
    public static class FileMonitoringEx
    {
        public static IObservable<FileNotification> Monitoring([NotNull]this FileInfo file, TimeSpan? refreshPeriod = null, IScheduler scheduler = null)
        {
            return Observable.Create<FileNotification>(observer =>
            {
                var refresh = refreshPeriod ?? TimeSpan.FromMilliseconds(250);
                scheduler = scheduler ?? Scheduler.Default;

                FileNotification notification = null;
                return scheduler.ScheduleRecurringAction(refresh, () =>
                {
                    try
                    {
                        notification = notification == null ? new FileNotification(file) : new FileNotification(notification);
                        observer.OnNext(notification);
                    }
                    catch (Exception ex)
                    {
                        notification = new FileNotification(file, ex);
                        observer.OnNext(notification);
                    }
                });

            }).DistinctUntilChanged();
        }
    }
}
