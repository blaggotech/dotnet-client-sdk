using FluentAssertions;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Xunit;

namespace Blackbox.Tests
{
    public class BlackboxShould
    {

        // user credentials
        private string username = "09955621981";
        private string password = "P@ssw0rd";
        private string baseAuthUrl = "https://authtest.blaggo.io/auth/";

        // Test Map:
        // 1. Arrange - Given
        // 2. Act - When
        // 3. Assert - Then

        [Fact]
        public async Task PrintAuthTokenIfCredentialsAreCorrect()
        {
            // authenticate first on blaggo auth url.
            var blaggo = new Blaggo(baseAuthUrl, username, password);
            var result = await blaggo.GetAuthToken();
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task NotPrintAuthTokenIfCredentialsAreInvalid()
        {
            // authenticate first on blaggo auth url.
            var invalidUsername = "_invalidUsername";
            var invalidPassword = "!validP4ssw0rd";
            var blaggo = new Blaggo(baseAuthUrl, invalidUsername,invalidPassword);
            var result = await blaggo.GetAuthToken();
            result.Should().NotBeNull();
        }
    }
}