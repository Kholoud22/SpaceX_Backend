using MediatR;
using SpaceX.Infrastructure.Launches.Dtos;

namespace SpaceX.Application.Launches.Queries
{
    public class GetLaunchQuery : IRequest<LaunchResponseDto>
    {
        public GetLaunchQuery(string id)
        {
            Id = id;
        }
        public string Id { get; private set; }
    }
}
