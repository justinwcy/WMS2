using Application.DTO.Response;

using MediatR;

namespace Application.Service.Queries
{
    public record GetAllCompanyQuery() : IRequest<IEnumerable<GetCompanyResponseDTO>>;
}
