using Application.DTO.Response;

using MediatR;

namespace Application.Service.Queries
{
    public record GetVendorByIdQuery(Guid Id) : IRequest<GetVendorResponseDTO>;
}
