using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System;
using Xunit;
using System.Net;
using System.Net.Http;

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

            HttpClient httpClient = new HttpClient();
            AuthResponse? authResponse = await blaggo.GetAuthToken(httpClient);

            _ = (authResponse?.Data.Should().NotBeNull());
            _ = (authResponse?.Data.UserId.Should().NotBeEmpty());
        }

        [Fact]
        public async Task GetProtocolPayloads()
        {
            // authenticate first on blaggo auth url.
            var blaggo = new Blaggo(baseAuthUrl, username, password);

            HttpClient httpClient = new HttpClient();
            AuthResponse? authResponse = await blaggo.GetAuthToken(httpClient);

            _ = (authResponse?.Data.Should().NotBeNull());
            _ = (authResponse?.Data.UserId.Should().NotBeEmpty());

            var accessToken = authResponse?.Data.Tokens.AccessToken;
            var blackbox = new Blackbox(accessToken);

            var response = await blackbox.GetPayloads(httpClient);

            _ = (response?.Should().NotBeNull());
            _ = (response?.Payloads.Should().NotBeEmpty());
        }

        [Fact]
        public async Task GetProtocolPayloadById()
        {
            var blaggo = new Blaggo(baseAuthUrl, username, password);

            HttpClient httpClient = new HttpClient();
            AuthResponse? authResponse = await blaggo.GetAuthToken(httpClient);

            _ = (authResponse?.Data.Should().NotBeNull());
            _ = (authResponse?.Data.UserId.Should().NotBeEmpty());

            var accessToken = authResponse?.Data.Tokens.AccessToken;
            var blackbox = new Blackbox(accessToken);

            string? protocolId = Environment.GetEnvironmentVariable("PROTOCOL_ID");
            var response = await blackbox.GetPayload(httpClient, protocolId);

            _ = (response?.Should().NotBeNull());
            _ = (response?.Payload.Should().NotBeNull());
        }

        [Fact]
        public async Task GetQuerySubscribers()
        {
            var blaggo = new Blaggo(baseAuthUrl, username, password);

            HttpClient httpClient = new HttpClient();
            AuthResponse? authResponse = await blaggo.GetAuthToken(httpClient);

            _ = (authResponse?.Data.Should().NotBeNull());
            _ = (authResponse?.Data.UserId.Should().NotBeEmpty());

            var accessToken = authResponse?.Data.Tokens.AccessToken;
            var blackbox = new Blackbox(accessToken);

            var response = await blackbox.GetSubscribers(httpClient);

            _ = (response?.Should().NotBeNull());
            _ = (response?.Accounts.Should().NotBeEmpty());
        }

        [Fact]
        public async Task GetInboxMessages()
        {
            var blaggo = new Blaggo(baseAuthUrl, username, password);

            HttpClient httpClient = new HttpClient();
            AuthResponse? authResponse = await blaggo.GetAuthToken(httpClient);

            _ = (authResponse?.Data.Should().NotBeNull());
            _ = (authResponse?.Data.UserId.Should().NotBeEmpty());

            var accessToken = authResponse?.Data.Tokens.AccessToken;
            var blackbox = new Blackbox(accessToken);

            var response = await blackbox.GetInbox(httpClient);

            _ = (response?.Should().NotBeNull());
            _ = (response?.Messages.Should().NotBeEmpty());
        }

        [Fact]
        public async Task DeleteInboxMessageById()
        {
            var blaggo = new Blaggo(baseAuthUrl, username, password);

            HttpClient httpClient = new HttpClient();
            AuthResponse? authResponse = await blaggo.GetAuthToken(httpClient);

            _ = (authResponse?.Data.Should().NotBeNull());
            _ = (authResponse?.Data.UserId.Should().NotBeEmpty());

            var accessToken = authResponse?.Data.Tokens.AccessToken;
            Blackbox? blackbox = new Blackbox(accessToken);

            string? protocolId = Environment.GetEnvironmentVariable("ID_TO_BE_DELETED");
            await blackbox?.DeleteInboxById(httpClient, protocolId);
        }

        [Fact]
        public async Task DeleteSubscriberByID()
        {
            var blaggo = new Blaggo(baseAuthUrl, username, password);

            HttpClient httpClient = new HttpClient();
            AuthResponse? authResponse = await blaggo.GetAuthToken(httpClient);

            _ = (authResponse?.Data.Should().NotBeNull());
            _ = (authResponse?.Data.UserId.Should().NotBeEmpty());

            var accessToken = authResponse?.Data.Tokens.AccessToken;
            Blackbox? blackbox = new Blackbox(accessToken);

            string? subscriberId = Environment.GetEnvironmentVariable("ID_TO_BE_DELETED");
            await blackbox?.DeleteSubscriber(httpClient, subscriberId);
        }

        [Fact]
        public async Task DeleteProtocolPayload()
        {
            var blaggo = new Blaggo(baseAuthUrl, username, password);

            HttpClient httpClient = new HttpClient();
            AuthResponse? authResponse = await blaggo.GetAuthToken(httpClient);

            _ = (authResponse?.Data.Should().NotBeNull());
            _ = (authResponse?.Data.UserId.Should().NotBeEmpty());

            var accessToken = authResponse?.Data.Tokens.AccessToken;
            Blackbox? blackbox = new Blackbox(accessToken);

            string? protocolId = Environment.GetEnvironmentVariable("ID_TO_BE_DELETED");
            await blackbox?.DeleteProtocolPayload(httpClient, protocolId);
        }
    }
}