namespace MOHU.Integration.WebApi.Features.Tickets.Dtos.Requests;

public record ValidateCategoriesRequest(
    List<Guid> CategoryIds);
