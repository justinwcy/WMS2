using Application.DTO.Response;

using MediatR;

namespace Application.Service.Queries
{
    public record GetCompaniesByIdsQuery(List<Guid> CompanyIds) : IRequest<List<GetCompanyResponseDTO>>;
}
