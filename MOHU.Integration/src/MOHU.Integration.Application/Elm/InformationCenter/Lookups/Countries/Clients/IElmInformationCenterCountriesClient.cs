using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Clients;

public interface IElmInformationCenterCountriesClient
{
    ErrorOr<List<ElmCountryResponse>> GetAll(ElmFilterRequest? request = null);
}