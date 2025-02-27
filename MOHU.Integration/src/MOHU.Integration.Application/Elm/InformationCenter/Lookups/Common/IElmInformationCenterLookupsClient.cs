using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;

public interface IElmInformationCenterLookupsClient
{
    public ErrorOr<TLookupData> GetLookups<TLookupData>(
        FilterRequest? request = null);
}