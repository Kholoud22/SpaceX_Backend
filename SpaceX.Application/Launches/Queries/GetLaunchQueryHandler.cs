using MediatR;
using SpaceX.Infrastructure.Proxies;
using SpaceX.Infrastructure.Launches.Dtos;

namespace SpaceX.Application.Launches.Queries
{
    public class GetLaunchQueryHandler : IRequestHandler<GetLaunchQuery, LaunchResponseDto>
    {
        private readonly ILaunchProxy _launchProxy;
        public GetLaunchQueryHandler(ILaunchProxy launchProxy)
        {
            _launchProxy = launchProxy;
        }

        public async Task<LaunchResponseDto> Handle(GetLaunchQuery query, CancellationToken cancellationToken)
        {            
            var result = await _launchProxy.Get(query.Id);

            return result;
        }
    }
}
