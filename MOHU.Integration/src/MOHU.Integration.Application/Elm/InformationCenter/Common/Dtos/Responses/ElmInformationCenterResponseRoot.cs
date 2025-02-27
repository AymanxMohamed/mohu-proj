namespace MOHU.Integration.Application.Elm.InformationCenter.Common.Dtos.Responses;

public class ElmInformationCenterResponseRoot<TData>
{
    public ElmInformationCenterResponseBase<TData> Response { get; init; } = null!;
    
    public ErrorOr<TData> EnsureSuccessResult() => Response.EnsureSuccessResult();
}