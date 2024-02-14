
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Logger
{
    [Flags]
    public enum SeverityLevel
    {
        None = 0x0,
        Info = 0x1,
        Warning = 0x2,
        Error = 0x4
    }

    public interface ILogger
    {

        void LogComment(string methodName, string message, SeverityLevel logSeverity);

        void LogException(string methodName, System.Exception exception);

        void FlushLogs();
    }

    public class LoggerHandler : ILogger
    {
        Notifier _notifier;
        CrmLogger _crmLogger;

        public LoggerHandler(bool isLoggingInfoEnabled, bool isLoggingWarningsEnabled, bool isLoggingErrorsEnabled, IOrganizationService organizationService)
        {
            var severityLevel = SeverityLevel.None;
            if (isLoggingInfoEnabled)
                severityLevel |= SeverityLevel.Info;
            if (isLoggingWarningsEnabled)
                severityLevel |= SeverityLevel.Warning;
            if (isLoggingErrorsEnabled)
                severityLevel |= SeverityLevel.Error;

            _crmLogger = new CrmLogger(organizationService);

            _notifier = new Notifier(severityLevel);
            _notifier.Subscribe(_crmLogger);

        }

        public void FlushLogs()
        {
            _notifier.FlushLogs();
        }

        public void LogComment(string message, SeverityLevel logSeverity)
        {
            LogComment(GetMethodFullName(2), message, logSeverity);
        }

        public void LogComment(string methodName, string message, SeverityLevel logSeverity)
        {
            _notifier.LogComment(methodName, message, logSeverity);
        }

        public void LogException(string methodName, System.Exception exception)
        {
            _notifier.LogException(methodName, exception);
        }

        public static string GetMethodFullName()
        {
            var stackTrace = new StackTrace();
            var methodBase = stackTrace.GetFrame(1).GetMethod();
            return methodBase.ReflectedType.FullName;
        }

        public static string ExtractInfo(Microsoft.Xrm.Sdk.OrganizationServiceFault organizationServiceFault )
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("The application terminated with an error.");
            stringBuilder.AppendLine($"Timestamp: {organizationServiceFault.Timestamp}");
            stringBuilder.AppendLine($"Code: {organizationServiceFault.ErrorCode}");
            stringBuilder.AppendLine($"Message: { organizationServiceFault.Message}");
            stringBuilder.Append(string.Format( "Inner Fault: {0}",
                    null == organizationServiceFault.InnerFault ? "No Inner Fault" : ExtractInfo(organizationServiceFault.InnerFault)));

            return stringBuilder.ToString();
        }

        public static string ExtractInfo(ExecuteMultipleResponse responseWithResults)
        {
            var stringBuilder = new StringBuilder();
            foreach (var responseItem in responseWithResults.Responses)
            {
                // A valid response.
                if (responseItem.Response != null)
                {
                    stringBuilder.AppendLine($"Request Index '{responseItem.RequestIndex}': Success");
                }
                // An error has occurred.
                else if (responseItem.Fault != null)
                {
                    stringBuilder.AppendLine($"Request Index '{responseItem.RequestIndex}': {responseItem.Fault.Message}");
                }

            }
            return stringBuilder.ToString();
        }

        public static string GetMethodFullName(int frameLevel)
        {
            var stackTrace = new StackTrace();
            var methodBase = stackTrace.GetFrame(1).GetMethod();
            return methodBase.ReflectedType.FullName;
        }

        public static string CommentAttributeCollection(AttributeCollection entityAttributes)
        {
            var historicalFields = new StringBuilder();
            foreach (var item in entityAttributes)
            {
                if (historicalFields.Length > 0)
                    historicalFields.Append($", ");
                if (item.Value == null)
                {
                    historicalFields.Append($"'{item.Key}':'null'");
                }
                else if (item.Value is EntityReference)
                {
                    var tmp = item.Value as EntityReference;
                    historicalFields.Append($"'{item.Key}':'{tmp.LogicalName}' '{tmp.Id}'");
                }
                else if (item.Value is OptionSetValue)
                {
                    var tmp = item.Value as OptionSetValue;
                    historicalFields.Append($"'{item.Key}':'{tmp.Value}'");
                }
                else
                {
                    historicalFields.Append($"'{item.Key}':'{item.Value}'");
                }

            }

            return historicalFields.ToString();
        }

        public static void AddLogToSomeCrmRecord(string methodName, EntityReference record, string logs,ILogger logger, IOrganizationService organizationService, string logFieldLogicalName = "ldv_logs")
        {
            try
            {
                string log = string.Empty;
                var query = new Microsoft.Xrm.Sdk.Query.QueryExpression()
                {
                    NoLock = true,
                    EntityName = record.LogicalName,
                    ColumnSet = new Microsoft.Xrm.Sdk.Query.ColumnSet(new[] { logFieldLogicalName }),
                    Criteria = new Microsoft.Xrm.Sdk.Query.FilterExpression()
                    { 
                        Conditions =
                        { 
                            new Microsoft.Xrm.Sdk.Query.ConditionExpression(record.LogicalName.ToLower()+"id",Microsoft.Xrm.Sdk.Query.ConditionOperator.Equal,record.Id)
                        }
                    }
                };

                var entity1 = organizationService.RetrieveMultiple(query).Entities.FirstOrDefault();                

                if (entity1.Contains(logFieldLogicalName))
                {
                    log = entity1[logFieldLogicalName].ToString() + logs;
                }
                else
                {
                    log = logs;
                }

                var entity2 = new Entity()
                {                    
                    LogicalName = entity1.LogicalName,
                    Id = entity1.Id,
                    Attributes = new AttributeCollection()
                    {
                        new KeyValuePair<string, object>(logFieldLogicalName,log)
                    }
                };

                //// Create an ExecuteMultipleRequest object.
                //var requestWithResults = new ExecuteMultipleRequest()
                //{
                //    // Assign settings that define execution behavior: continue on error, return responses. 
                //    Settings = new ExecuteMultipleSettings()
                //    {
                //        ContinueOnError = true,
                //        ReturnResponses = true
                //    },
                //    // Create an empty organization request collection.
                //    Requests = new OrganizationRequestCollection()
                //};
                //requestWithResults.Requests.Add(new UpdateRequest() { Target = entity2 });

                //// Execute all the requests in the request collection using a single web method call.
                //var responseWithResults =
                //    (ExecuteMultipleResponse)organizationService.Execute(requestWithResults);

                //if (!responseWithResults.IsFaulted)
                //    logger.LogComment(methodName, $"Logs added successfully to '{entity1.LogicalName}' with Id '{entity1.Id}'", LinkDev.Common.Crm.Logger.SeverityLevel.Info);
                //else
                //    logger.LogComment(methodName, $"Failed to add the log to current record\r\n{ExtractInfo(responseWithResults)}", LinkDev.Common.Crm.Logger.SeverityLevel.Error);

                organizationService.Update(entity2);
                logger.LogComment(methodName, $"Logs added successfully to '{entity1.LogicalName}' with Id '{entity1.Id}'", LinkDev.Common.Crm.Logger.SeverityLevel.Info);

            }
            catch (System.Exception exc)
            {
                logger.LogComment(methodName, $"Failed to add the log to current record.\r\n{exc}", LinkDev.Common.Crm.Logger.SeverityLevel.Error);
            }
        }

        public override string ToString()
        {
            return _notifier.ToString();
        }
    }

    class Notifier : IObservable<List<LogMessage>>, ILogger
    {
        List<IObserver<List<LogMessage>>> _observers;
        List<LogMessage> _logMessages;
        SeverityLevel _debugLevel;

        public Notifier(SeverityLevel debugLevel)
        {
            _debugLevel = debugLevel;
            _observers = new List<IObserver<List<LogMessage>>>();
            _logMessages = new List<LogMessage>();
        }

        public IDisposable Subscribe(IObserver<List<LogMessage>> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        public void FlushLogs()
        {
            foreach (var item in _observers)
            {
                item.OnNext(_logMessages);
                item.OnCompleted();
            }

            // Clear messages
            _logMessages.Clear();
        }


        public void LogComment(string message, SeverityLevel logSeverity)
        {
            var methodName = LoggerHandler.GetMethodFullName(2);
            if ((logSeverity & _debugLevel) != 0)
            {
                _logMessages.Add(new LogMessage(methodName, message, logSeverity));
            }
        }

        public void LogComment(string methodName, string message, SeverityLevel logSeverity)
        {

            if ((logSeverity & _debugLevel) != 0)
            {
                _logMessages.Add(new LogMessage(methodName, message, logSeverity));
            }
        }

        public void LogException(string methodName, System.Exception exception)
        {
            if (exception.InnerException != null)
            {
                LogComment(methodName, $"{exception.ToString()}", SeverityLevel.Error);
            }
            else
            {
                LogComment(methodName, $"{exception.Message}\r\n{exception.StackTrace}", SeverityLevel.Error);
            }
                
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in _logMessages)
            {
                stringBuilder.AppendLine(item.ToString());
            }
            return stringBuilder.ToString();
        }
    }

    class CrmLogger : IObserver<List<LogMessage>>
    {
        IOrganizationService _organizationService;
        public CrmLogger(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public void OnCompleted()
        {

            // Do nothing
        }

        public void OnError(System.Exception error)
        {
            // Do nothing
        }

        public void OnNext(List<LogMessage> value)
        {
            try
            {
                var entity = new Entity("ldv_log");
                var logRecordSeverity = SeverityLevel.None;
                var stringBuilder = new StringBuilder();
                LogMessage firstLogMessage = null;
                foreach (var item in value)
                {
                    if (firstLogMessage == null)
                    {
                        firstLogMessage = item;
                    }

                    switch (item.LogSeverity)
                    {
                        case SeverityLevel.Info:
                            if (logRecordSeverity == SeverityLevel.None)
                                logRecordSeverity = SeverityLevel.Info;
                            break;
                        case SeverityLevel.Warning:
                            if (logRecordSeverity == SeverityLevel.None || logRecordSeverity == SeverityLevel.Info)
                                logRecordSeverity = SeverityLevel.Warning;
                            break;
                        case SeverityLevel.Error:
                            if (logRecordSeverity == SeverityLevel.None || logRecordSeverity == SeverityLevel.Info || logRecordSeverity == SeverityLevel.Warning)
                                logRecordSeverity = SeverityLevel.Error;
                            break;
                        default:
                            break;
                    }

                    stringBuilder.AppendLine(item.ToString());
                }
                if (firstLogMessage != null)
                    entity.Attributes["ldv_name"] = firstLogMessage.MethodName;
                entity.Attributes["ldv_stacktrace"] = stringBuilder.ToString();
                switch (logRecordSeverity)
                {
                    case SeverityLevel.None:
                        entity.Attributes["ldv_logserverity"] = new OptionSetValue(753240000);
                        break;
                    case SeverityLevel.Info:
                        entity.Attributes["ldv_logserverity"] = new OptionSetValue(753240001);
                        break;
                    case SeverityLevel.Warning:
                        entity.Attributes["ldv_logserverity"] = new OptionSetValue(753240002);
                        break;
                    case SeverityLevel.Error:
                        entity.Attributes["ldv_logserverity"] = new OptionSetValue(753240003);
                        break;
                    default:
                        break;
                }

                // Create an ExecuteMultipleRequest object.
                var requestWithResults = new ExecuteMultipleRequest()
                {
                    // Assign settings that define execution behavior: continue on error, return responses. 
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = true,
                        ReturnResponses = true
                    },
                    // Create an empty organization request collection.
                    Requests = new OrganizationRequestCollection()
                };
                requestWithResults.Requests.Add(new CreateRequest() { Target = entity });

                // Execute all the requests in the request collection using a single web method call.
                var responseWithResults =
                    (ExecuteMultipleResponse)_organizationService.Execute(requestWithResults);


            }
            catch (System.Exception)
            {


            }
        }
    }

    class EventViewerLogger : ILogger
    {
        private const string eventViewerLogName = "LinkDev";
        EventLog eventLog;
        public EventViewerLogger()
        {
            eventLog = new EventLog();
        }

        public void FlushLogs()
        {
           
        }

        public void LogComment(string methodName, string message, SeverityLevel logSeverity)
        {
            try
            {
                if (!EventLog.SourceExists(methodName))
                {
                    System.Diagnostics.EventLog.CreateEventSource(
                        methodName, eventViewerLogName);
                }
                eventLog.Source = methodName;

                switch (logSeverity)
                {
                    case SeverityLevel.None:
                        eventLog.WriteEntry(message, EventLogEntryType.Information);
                        break;
                    case SeverityLevel.Info:
                        eventLog.WriteEntry(message, EventLogEntryType.Information);
                        break;
                    case SeverityLevel.Warning:
                        eventLog.WriteEntry(message, EventLogEntryType.Warning);
                        break;
                    case SeverityLevel.Error:
                        eventLog.WriteEntry(message, EventLogEntryType.Error);
                        break;
                    default:
                        eventLog.WriteEntry(message, EventLogEntryType.Information);
                        break;
                }
            }
            catch (System.Exception)
            {
            }


        }

        public void LogException(string methodName, System.Exception exception)
        {
            LogComment(methodName, $"{exception.Message}\r\n{exception.StackTrace}", SeverityLevel.Error);
        }


    }

   
    class LogMessage
    {
        public string MethodName;
        public string Message;
        public SeverityLevel LogSeverity;

        public LogMessage(string methodName, string message, SeverityLevel logSeverity)
        {
            MethodName = methodName;
            Message = message;
            LogSeverity = logSeverity;
        }

        public override string ToString()
        {
            return $"{DateTime.Now} | Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId} | {MethodName} | {LogSeverity.ToString()} | {Message}";
        }
    }

    class Unsubscriber : IDisposable
    {
        private List<IObserver<List<LogMessage>>> _observers;
        private IObserver<List<LogMessage>> _observer;

        public Unsubscriber(List<IObserver<List<LogMessage>>> observers, IObserver<List<LogMessage>> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
