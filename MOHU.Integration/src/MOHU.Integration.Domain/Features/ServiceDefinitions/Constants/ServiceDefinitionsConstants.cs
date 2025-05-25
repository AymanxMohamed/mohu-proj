namespace MOHU.Integration.Domain.Features.ServiceDefinitions.Constants;

public class ServiceDefinitionConstants
{
    public const string LogicalName = "ldv_service";
    
    public static class Fields
    {
        public const string Id = "ldv_serviceid";
        public const string Name = "ldv_name";
        public const string Code = "ldv_code";
        public const string EnglishName = "ldv_name_en";
        public const string ArabicName = "ldv_name_ar";
        public const string ParentService = "ldv_serviceparentid";
        public const string AutomaticResolveForApi = "ldv_automaticresolvebyapi";
    }

    public static class Services
    {
        public static readonly Guid ErshadService = new("7a8c2cbc-8f8c-ef11-ac20-6045bd8fae55");
        public static readonly Guid MafkodenService = new("f1cc8dda-8f8c-ef11-ac20-6045bd8fae55");
        public static readonly Guid SosService = new("f8921d0b-4233-f011-8c4d-7c1e522940f7");
        public static readonly Guid La7zyaHig = new("7b80a868-2dcc-ee11-907a-6045bd8c92a2");
        public static readonly Guid La7zyaUmrah = new("7980a868-2dcc-ee11-907a-6045bd8c92a2");
    }
}