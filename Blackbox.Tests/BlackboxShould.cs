using FluentAssertions;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System;
using Xunit;

namespace Blackbox.Tests
{
    public class BlackboxShould
    {

        // user credentials
        private string? username = Environment.GetEnvironmentVariable("ACCOUNT_USERNAME");
        private string? password = Environment.GetEnvironmentVariable("ACCOUNT_PASSWORD");
        private string? baseAuthUrl = Environment.GetEnvironmentVariable("AUTH_BASE_URL");

        private string? blackboxBaseURL = Environment.GetEnvironmentVariable("BLACKBOX_BASE_URL");

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
        public async Task GetProtocolPayloads()
        {
            var blaggo = new Blaggo(baseAuthUrl, username, password);
            AuthResponse authResponse = await blaggo.GetAuthToken();

            var accessToken = authResponse.Data.Tokens.AccessToken;
            var queryProtocolPayloadsURL = blackboxBaseURL + "/payloads";

            Blackbox blackbox = new Blackbox(queryProtocolPayloadsURL, accessToken);

            var payloads = await blackbox.GetPayloads();
            payloads.Payloads.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetProtocolPayloadById()
        {
            string? protocolId = Environment.GetEnvironmentVariable("PROTOCOL_ID");
            var blaggo = new Blaggo(baseAuthUrl, username, password);
            AuthResponse authResponse = await blaggo.GetAuthToken();

            var accessToken = authResponse.Data.Tokens.AccessToken;
            var getProtocolPayloadByIdUrl = blackboxBaseURL + "/payloads";

            Blackbox blackbox = new Blackbox(getProtocolPayloadByIdUrl, accessToken);
            var response = await blackbox.GetPayload(protocolId);

            response?.Payload.Should().NotBeNull();
        }

        [Fact]
        public async Task GetQuerySubscribers()
        {
            var blaggo = new Blaggo(baseAuthUrl, username, password);
            AuthResponse authResponse = await blaggo.GetAuthToken();

            var accessToken = authResponse.Data.Tokens.AccessToken;
            var getAccountsURL = blackboxBaseURL+"/accounts";

            Blackbox blackbox = new Blackbox(getAccountsURL, accessToken);

            var accounts = await blackbox.GetSubscribers();

            accounts?.Accounts.Should().NotBeEmpty();
        }

        [Fact]
        public async Task DeleteSubscriberByID()
        {
            string? IDToBeDeleted = Environment.GetEnvironmentVariable("ID_TO_BE_DELETED");

            // login to Blaggo
            var blaggo = new Blaggo(baseAuthUrl, username, password);
            AuthResponse authResponse = await blaggo.GetAuthToken();

            var accessToken = authResponse.Data.Tokens.AccessToken;
            var getAccountsURL = blackboxBaseURL + "/accounts";

            Blackbox blackbox = new Blackbox(getAccountsURL, accessToken);

            await blackbox?.DeleteSubscriber(IDToBeDeleted);
        }
    }
}