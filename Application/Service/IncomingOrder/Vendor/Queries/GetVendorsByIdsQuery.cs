using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetVendorsByIdsQuery(List<Guid> VendorIds) : IRequest<List<GetVendorResponseDTO>>;
}
