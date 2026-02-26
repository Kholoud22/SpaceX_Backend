using MediatR;
using SpaceX.Infrastructure.Launches.Dtos;
using SpaceX.Application.Enums;

namespace SpaceX.Application.Launches.Queries
{
    public class GetLaunchesQuery : IRequest<List<LaunchResponseDto>>
    {
        public GetLaunchesQuery(LaunchPeriods launchPeriods)
        {
            Period = launchPeriods.ToString();
        }
        public string Period { get; private set; }
    }
}
