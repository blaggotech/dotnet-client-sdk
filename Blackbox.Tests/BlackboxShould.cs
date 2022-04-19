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

        private string blackboxBaseURL = "https://blackboxtest.blaggo.io";

        // Test Map:
        // 1. Arrange - Given
        // 2. Act - When
        // 3. Assert - Then

        [Fact]
        public async Task PrintAuthTokenIfCredentialsAreCorrect()
        {
            // authenticate first on blaggo auth url.
            var blaggo = new Blaggo(baseAuthUrl, username, password);
            AuthResponse authResponse = await blaggo.GetAuthToken();

            authResponse.Data.Tokens.AccessToken.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetQuerySubscribers()
        {
            var blaggo = new Blaggo(baseAuthUrl, username, password);
            AuthResponse authResponse = await blaggo.GetAuthToken();
            var accessToken = authResponse.Data.Tokens.AccessToken;
            var getAccountsURL = blackboxBaseURL+"/accounts";

            Blackbox blackbox = new Blackbox(accessToken);

            var accounts = await blackbox.GetSubscribers(getAccountsURL);
        }
    }
}