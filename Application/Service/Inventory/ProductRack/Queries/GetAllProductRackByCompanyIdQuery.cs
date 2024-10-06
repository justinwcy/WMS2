using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllProductRackByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetProductRackResponseDTO>>;
}
    