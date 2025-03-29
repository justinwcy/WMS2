using MediatR;

namespace Application.Interface
{
    public interface ICachedQuery
    {
        public string Key { get; }
        public TimeSpan? Expiration { get; }
    }

    public interface ICachedQuery<TResponse> : ICachedQuery, IRequest<TResponse>
    {

    }
}
