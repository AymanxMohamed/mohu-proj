using MOHU.Integration.Application.Elm.InformationCenter.Common.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;

internal class ElmInformationCenterApplicantDataClient(IElmInformationCenterClient client) 
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: $"{LookupsConstants.MainCollectionName}/applicant-data",
            client), 
        IElmInformationCenterApplicantDataClient
{
    public ErrorOr<List<ElmApplicant>> GetAll(FilterRequest? request = null) => 
        GetLookups<List<ApplicantResponse>>(request)
            .Then(x => x.Select(ElmApplicant.Create).ToList());
}