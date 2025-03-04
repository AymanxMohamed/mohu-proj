using MOHU.Integration.Application.Elm.InformationCenter.Common.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Dtos.Responses;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Models.ElmApplicants;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;
using Individual = MOHU.Integration.Domain.Individuals.Individual;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.Applicants.Clients;

internal class ElmInformationCenterApplicantDataClient(IElmInformationCenterClient client) 
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: $"{LookupsConstants.MainCollectionName}/applicant-data",
            client), 
        IElmInformationCenterApplicantDataClient
{
    public ErrorOr<List<Individual>> GetAll(FilterRequest? request = null) => 
        GetLookups<List<ApplicantResponse>>(request)
            .Then(x => x.Select(y => y.ToIndividual()).ToList());
}