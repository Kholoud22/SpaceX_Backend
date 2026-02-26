using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using SpaceX.Application.Launches.Queries;
using System.Collections.Generic;
using SpaceX.Infrastructure.Launches.Dtos;
using System.Linq;
using System.IO;
using SpaceX.Infrastructure.Exceptions;
using SpaceX.Application.Enums;

namespace SpaceX.Test
{
    public class GetLaunchesQueryTest : ApplicationTestBase
    {
        public GetLaunchesQueryTest()
        {
        }

        [Fact]
        public async Task ShouldGetUpcomingLaunches()
        {
            GetLaunchesQuery query = new GetLaunchesQuery(LaunchPeriods.upcoming);
            var result = await SendAsync(query);

            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ShouldGetPastLaunches()
        {
            GetLaunchesQuery query = new GetLaunchesQuery(LaunchPeriods.past);
            var result = await SendAsync(query);

            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ShouldGetLaunchById()
        {
            GetLaunchesQuery query = new GetLaunchesQuery(LaunchPeriods.past);
            var result = await SendAsync(query);
            result.Should().NotBeEmpty();

            var launchQuery = new GetLaunchQuery(result.First().Id);
            var launch = await SendAsync(launchQuery);
            launch.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldThrowException()
        {
            var launchQuery = new GetLaunchQuery("123");

            var ex = await Assert.ThrowsAsync<IntegrationException>(async () => await SendAsync(launchQuery));
            Assert.Equal("Not Found", ex.Message);

        }
    }
}
