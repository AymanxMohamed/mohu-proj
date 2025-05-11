namespace MOHU.Integration.Application.Elm.ServiceDesk.Common.Dtos.Responses;

public class ElmInformationCenterResponseRoot<TData>
{
    public ElmInformationCenterResponseBase<TData> Response { get; init; } = null!;
    
    public ErrorOr<TData> EnsureSuccessResult() => Response.EnsureSuccessResult();
}