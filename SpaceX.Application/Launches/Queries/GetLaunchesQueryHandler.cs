using MediatR;
using SpaceX.Infrastructure.Proxies;
using SpaceX.Infrastructure.Launches.Dtos;

namespace SpaceX.Application.Launches.Queries
{
    public class GetLaunchesQueryHandler : IRequestHandler<GetLaunchesQuery, List<LaunchResponseDto>>
    {
        private readonly ILaunchProxy _launchProxy;
        public GetLaunchesQueryHandler(ILaunchProxy launchProxy)
        {
            _launchProxy = launchProxy;
        }

        public async Task<List<LaunchResponseDto>> Handle(GetLaunchesQuery query, CancellationToken cancellationToken)
        {            
            var result = await _launchProxy.GetAll(query.Period.ToString());

            return result;
        }
    }
}
