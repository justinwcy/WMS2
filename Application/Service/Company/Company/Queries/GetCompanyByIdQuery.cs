using Application.DTO.Response;

using MediatR;

namespace Application.Service.Queries
{
    public record GetCompanyByIdQuery(Guid Id) : IRequest<GetCompanyResponseDTO>;
}
