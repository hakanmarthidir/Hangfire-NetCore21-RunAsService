using Hangfire;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HangfireAspNetCore21RunasService
{
    internal class HangHostService : IHostedService, IDisposable
    {
        public HangHostService() { }

        [AutomaticRetry(Attempts = 2, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public Task StartAsync(CancellationToken cancellationToken)
        {
            RecurringJob.AddOrUpdate(() => WriteToFileHangfireLog(), Cron.Minutely);
            return Task.CompletedTask;
        }

        //Write Your Operation
        public void WriteToFileHangfireLog()
        {
            //Sample
            using (StreamWriter writer = new StreamWriter("C:\\hangfire.txt", true))
            {
                writer.WriteLine("Sample Work " + DateTime.Now.ToString());
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
