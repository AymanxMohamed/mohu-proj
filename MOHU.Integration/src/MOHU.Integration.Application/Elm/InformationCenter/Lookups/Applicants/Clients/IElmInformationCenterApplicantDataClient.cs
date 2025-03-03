using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;

public interface IElmInformationCenterApplicantDataClient
{
    ErrorOr<List<ElmApplicant>> GetAll(FilterRequest? request = null);
}