using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetAllVendorByCompanyIdQuery(Guid CompanyId) : IRequest<IEnumerable<GetVendorResponseDTO>>;
}
