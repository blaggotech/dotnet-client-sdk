using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System;
using Xunit;
using System.Net;
using System.Net.Http;

namespace BlaggoBlackbox.Tests
{
    public class BlackboxShould
    {

        // user credentials
        public string? username = Environment.GetEnvironmentVariable("ACCOUNT_USERNAME");
        public string? password = Environment.GetEnvironmentVariable("ACCOUNT_PASSWORD");
        public string? baseAuthUrl = Environment.GetEnvironmentVariable("AUTH_BASE_URL");

        public string? blackboxBaseURL = Environment.GetEnvironmentVariable("BLACKBOX_BASE_URL");

        private HttpClient _httpClient;
        private Credentials _credentials;
        private Options _options;

        public BlackboxShould()
        {
            _httpClient = new HttpClient();
            _credentials = new Credentials()
            {
                Username = username,
                Password = password
            };
            _options = new Options()
            {
                AuthURL = baseAuthUrl,
                Credentials = _credentials,
                AuthenticatorFn = null,
                HttpClient = _httpClient
            };
        }

        [Fact]
        public async Task PrintAuthTokenIfCredentialsAreCorrect()
        {
            // 1. Arrange - Given
            var blaggo = new Blaggo(_options);

            // 2. Act - When
            AuthResponse? authResponse = await blaggo.AuthFn(_options.AuthURL, _credentials);

            // 3. Assert - Then
            _ = (authResponse?.Data.Should().NotBeNull());
            _ = (authResponse?.Data.UserId.Should().NotBeEmpty());
        }

        [Fact]
        public async Task GetProtocolPayloads()
        {
            var blackbox = new Blackbox(_options);
            var response = await blackbox.GetPayloads();

            _ = (response?.Should().NotBeNull());
            _ = (response?.Payloads.Should().NotBeEmpty());
        }

        [Fact]
        public async Task GetProtocolPayloadById()
        {
            string? protocolId = Environment.GetEnvironmentVariable("PROTOCOL_ID");

            var blackbox = new Blackbox(_options);
            var response = await blackbox.GetPayload(protocolId);

            _ = (response?.Should().NotBeNull());
            _ = (response?.Payload.Should().NotBeNull());
        }

        [Fact]
        public async Task GetSubscribers()
        {
            var blackbox = new Blackbox(_options);
            var response = await blackbox.GetSubscribers();

            _ = (response?.Should().NotBeNull());
            _ = (response?.Accounts.Should().NotBeEmpty());
        }
    }
}
