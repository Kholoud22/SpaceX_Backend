using SpaceX.Infrastructure.Launches.Dtos;

namespace SpaceX.Infrastructure.Proxies
{
    public interface ILaunchProxy
    {
        Task<List<LaunchResponseDto>> GetAll(string period);
        Task<LaunchResponseDto> Get(string id);
    }
}
