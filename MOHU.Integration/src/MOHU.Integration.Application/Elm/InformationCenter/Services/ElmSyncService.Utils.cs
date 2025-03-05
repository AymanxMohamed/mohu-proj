namespace MOHU.Integration.Application.Elm.InformationCenter.Services;

public partial class ElmSyncService<TElmClient, TElmEntity, TCrmEntity>
{
    public string GetSyncKey() => $"Last_Synced_{typeof(TElmEntity).Name}_Page";
}