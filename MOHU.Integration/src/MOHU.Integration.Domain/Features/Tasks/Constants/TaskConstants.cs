namespace MOHU.Integration.Domain.Features.Tasks.Constants;

public static class TaskConstants
{
    public const string LogicalName = "task";

    public static class Fields
    {
        public const string Id = "taskid";
        public const string Subject = "subject";
        public const string Decision = "ldv_decision";
        public const string DecisionMadeBy = "ldv_decisionmadebyuseridid";
        public const string TaskType = "ldv_tasktypecode";
        public const string ProcessingTimeInMinutes = "ldv_closeddurationinminutes";
        public const string Comment = "ldv_additioncomment";
        public const string ActualEnd = "actualend";
        public const string IsResolvedBySla = "ldv_isresolvedwithinthesla";
        public const string IsSendEscalationL1 = "ldv_issendescalationl1";
        public const string IsSendEscalationL2 = "ldv_issendescalationl2";
        public const string IsSendEscalationL3 = "ldv_issendescalationl3";
        public const string SlaLevelOneTimer = "ldv_slatasktimerid";
        public const string SlaLevelTwoTimer = "ldv_slatasktimermediumid";
        public const string SlaLevelThreeTimer = "ldv_slatasktimerlowid";
        
        public const string Priority = "prioritycode";
        public const string Regarding = "regardingobjectid";
   
    }

    public static class RelatedEntities
    {
        public static class SlaKpiInstance
        {
            public static class SlaLevelOneTimer
            {
                public const string Alies = "LevelOneTimer";
            } 
            
            public static class SlaLevelTwoTimer
            {
                public const string Alies = "LevelTwoTimer";
            } 
            
            public static class SlaLevelThreeTimer
            {
                public const string Alies = "LevelThreeTimer";
            } 
        }
    }
}
