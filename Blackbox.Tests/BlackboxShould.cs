using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System;
using Xunit;
using Moq;
using Moq.Protected;
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
            var blaggo = new Blaggo(baseAuthUrl, username: username, password: password);

            HttpClient httpClient = new HttpClient();
            AuthResponse? authResponse = await blaggo.GetAuthToken(httpClient);

            _ = (authResponse?.Data.Should().NotBeNull());
            _ = (authResponse?.Data.UserId.Should().NotBeEmpty());
        }
    }
}