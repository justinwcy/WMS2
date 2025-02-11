using Application.Interface;
using Application.Service.Queries;
using MediatR;

namespace Infrastructure.Repository
{
    public class IsFileExistHandler(IFileStorage fileStorage) : 
        IRequestHandler<IsFileExistQuery, bool>
    {
        public async Task<bool> Handle(IsFileExistQuery request, CancellationToken cancellationToken)
        {
            return await fileStorage.IsFileExist(request.FileName);

        }
    }
}
