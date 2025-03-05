using MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Requests;

namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Clients;

public interface IElmEntityClient<TElmEntity>
{
    ErrorOr<List<TElmEntity>> GetAll(ElmFilterRequest? request = null);
}