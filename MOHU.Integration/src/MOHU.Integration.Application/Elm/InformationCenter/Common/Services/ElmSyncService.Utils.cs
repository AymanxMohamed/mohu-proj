namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Services;

public partial class ElmSyncService<TElmClient, TElmEntity, TCrmEntity>
{
    private static string GetSyncKey() => $"Last_Synced_{typeof(TElmEntity).Name}_Page";
}