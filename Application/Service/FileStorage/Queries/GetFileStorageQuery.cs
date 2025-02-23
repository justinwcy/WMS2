using Application.DTO.Response;
using MediatR;

namespace Application.Service.Queries
{
    public record GetFileStorageQuery(Guid FileStorageId) : IRequest<GetFileStorageResponseDTO>;
}
