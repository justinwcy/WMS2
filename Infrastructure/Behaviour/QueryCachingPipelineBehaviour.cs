using Application.Interface;
using Infrastructure.Caching;
using MediatR;

namespace Infrastructure.Behaviour
{
    internal sealed class QueryCachingPipelineBehaviour<TRequest, TResponse>(ICachedService cachedService)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICachedQuery
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            return await cachedService.GetOrCreateAsync(
                request.Key, 
                _ => next(), 
                request.Expiration,
                cancellationToken);
        }
    }
}
