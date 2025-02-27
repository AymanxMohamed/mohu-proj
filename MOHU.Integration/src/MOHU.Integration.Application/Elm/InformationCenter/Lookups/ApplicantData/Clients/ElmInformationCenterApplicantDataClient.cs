using MOHU.Integration.Application.Elm.InformationCenter.Common.Clients;
using MOHU.Integration.Application.Elm.InformationCenter.Lookups.Common;

namespace MOHU.Integration.Application.Elm.InformationCenter.Lookups.ApplicantData.Clients;

internal class ElmInformationCenterApplicantDataClient(IElmInformationCenterClient client) 
    : ElmInformationCenterLookupsClient(
            lookupCollectionName: $"{LookupsConstants.MainCollectionName}/applicant-data",
            client), 
        IElmInformationCenterApplicantDataClient;