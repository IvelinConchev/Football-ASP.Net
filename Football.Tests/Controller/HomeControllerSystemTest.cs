namespace Football.Tests.Controller
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.VisualStudio.TestPlatform.TestHost;
    using System.Threading.Tasks;
    using Xunit;

    public class HomeControllerSystemTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;

        public HomeControllerSystemTest(WebApplicationFactory<Program> factory)
            => this.factory = factory;

        [Fact]
        public async Task IndexShouldReturnCorrectResult()
        {
            //Arrange
            var client = this.factory.CreateClient();

            //Act
            var result = await client.GetAsync("/");

            //Assert

            Assert.True(result.IsSuccessStatusCode);
        }
    }
}
