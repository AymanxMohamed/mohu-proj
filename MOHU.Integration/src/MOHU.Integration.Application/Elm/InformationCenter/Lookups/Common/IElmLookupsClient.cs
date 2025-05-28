namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;

public interface IElmLookupsClient
{
    ErrorOr<TLookupData> GetLookups<TLookupData>(
        string lookupCollectionName,
        ElmFilterRequest? request = null);
}