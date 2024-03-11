using MOHU.Integration.Contracts.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Infrastructure.Service
{
    public class SerilogLogger : IAppLogger
    {

        public async Task LogError(string message, params object[] args)
        {
            Log.Error(message, args);
            await Task.CompletedTask;
        }

        public async Task LogError(Exception e)
        {
            Log.Error("Exception {source} {message} {stacktrace} ", e.Source, e.Message, e.StackTrace);
            await Task.CompletedTask;
        }

        public async Task LogError(string messageTemplate, Exception exception)
        {
            Log.Error(exception, messageTemplate);
            await Task.CompletedTask;
        }

        public async Task LogInfo(string message, params object[] args)
        {
            Log.Information(message, args);
            await Task.CompletedTask;
        }

        public async Task LogInfo(string message)
        {
            Log.Information(message);
            await Task.CompletedTask;
        }

        public async Task LogWarning(string message, params object[] options)
        {
            Log.Warning(message, options);
            await Task.CompletedTask;
        }

        public async Task LogWarning(string message)
        {
            Log.Warning(message);
            await Task.CompletedTask;
        }

        public async Task LogWarning(string message, Exception exception)
        {
            Log.Warning(exception, message);
            await Task.CompletedTask;
        }
    }



}
