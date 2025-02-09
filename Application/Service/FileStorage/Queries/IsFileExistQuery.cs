using MediatR;

namespace Application.Service.Queries
{
    public record IsFileExistQuery(string FileName) : IRequest<bool>;
}
