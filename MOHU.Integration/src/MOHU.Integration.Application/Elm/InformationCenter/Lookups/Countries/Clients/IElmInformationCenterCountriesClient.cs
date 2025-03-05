using MOHU.Integration.Application.Elm.InformationCenter.Common.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Countries.Clients;

public interface IElmInformationCenterCountriesClient : IElmEntityClient<ElmCountryResponse>;
