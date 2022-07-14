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
            HttpClient httpClient = new HttpClient();
            Credentials creds = new Credentials
            {
                Username = username,
                Password = password,
            };

            Options options = new Options
            {
                AuthURL = baseAuthUrl,
                Credentials = creds,
                AuthenticatorFn = null,
                HttpClient = httpClient
            };

            // authenticate first on blaggo auth url.
            var blaggo = new Blaggo(options);

            
            AuthResponse? authResponse = await blaggo.AuthFn(options.AuthURL, creds);

            _ = (authResponse?.Data.Should().NotBeNull());
            _ = (authResponse?.Data.UserId.Should().NotBeEmpty());
        }
    }
}
