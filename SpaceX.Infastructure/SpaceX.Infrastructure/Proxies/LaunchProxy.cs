using SpaceX.Infrastructure.Launches.Dtos;

namespace SpaceX.Infrastructure.Proxies
{
    public class LaunchProxy : ILaunchProxy
    {
        private readonly IHttpRequestService _httpRequestService;

        public LaunchProxy(IHttpRequestService httpRequestService)
        {
            _httpRequestService = httpRequestService;
        }
        public async Task<LaunchResponseDto> Get(string id)
        {
            var result = await _httpRequestService.GetResponseInJson<LaunchResponseDto>($"launches/{id}");
            return result;
        }

        public async Task<List<LaunchResponseDto>> GetAll(string period)
        {
            var result = await _httpRequestService.GetResponseInJson<List<LaunchResponseDto>>($"launches/{period}");
            return result;
        }
    }
}
