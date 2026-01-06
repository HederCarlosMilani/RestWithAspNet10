using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scafold.Tests.IntegrationTests.Tools;

namespace RestWithAspNet10Scafold.Tests.IntegrationTests.Auth;


[TestCaseOrderer("RestWithAspNet10Scafold.Tests.IntegrationTests.Tools.PriorityOrderer",
    "RestWithAspNet10Scafold.Tests")]
public class AuthControllerIntegrationTests : IClassFixture<SqlServerFixture>
{
    private readonly HttpClient _client;
    private static AccountCredentialsDto? _accountCredentialsDto;
    private static TokenDto? _tokenDto;

    public AuthControllerIntegrationTests(SqlServerFixture sqlFixture)
    {
        var factory = new CustomWebApplicationFactory<Program>(sqlFixture.ConnectionString);
        _client = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("http://localhost")
            });
    }

    [Fact(DisplayName = "01 - Create Account Credentials"), TestPriority(1)]
    public async Task TestCreateAccountCredentials()
    {
        // Arrange
        var accountCredentialsToCreate = new AccountCredentialsDto
        {
            UserName = "testuser",
            Password = "test1234",
            FullName = "Test User"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/auth/create", accountCredentialsToCreate);

        // Assert
        response.EnsureSuccessStatusCode();

        var createdAccountCredentials = await response.Content.ReadFromJsonAsync<AccountCredentialsDto>();
        createdAccountCredentials.Should().NotBeNull();
        createdAccountCredentials!.UserName.Should().Be(accountCredentialsToCreate.UserName);
        createdAccountCredentials!.FullName.Should().Be(accountCredentialsToCreate.FullName);

        _accountCredentialsDto = createdAccountCredentials;
        _accountCredentialsDto.Password = accountCredentialsToCreate.Password; // Keep the plain password for login
    }

    [Fact(DisplayName = "02 - Sign In"), TestPriority(2)]
    public async Task TestSignIn()
    {
        // Arrange
        var userDto = new UserDto
        {
            UserName = _accountCredentialsDto!.UserName,
            Password = _accountCredentialsDto.Password
        };

        // Act
        var response = await _client.PostAsJsonAsync("/auth/signin", userDto);

        // Assert
        response.EnsureSuccessStatusCode();

        var token = await response.Content.ReadFromJsonAsync<TokenDto>();
        token.Should().NotBeNull();
        token!.AccessToken.Should().NotBeNullOrWhiteSpace();
        token.RefreshToken.Should().NotBeNullOrWhiteSpace();

        _tokenDto = token;
    }

    [Fact(DisplayName = "03 - Refresh Token"), TestPriority(3)]
    public async Task TestRefreshToken()
    {
        // Arrange
        var tokenDto = _tokenDto;

        // Act
        var response = await _client.PostAsJsonAsync("/auth/refresh", tokenDto);

        // Assert
        response.EnsureSuccessStatusCode();
        var refreshedToken = await response.Content.ReadFromJsonAsync<TokenDto>();
        refreshedToken.Should().NotBeNull();
        refreshedToken!.AccessToken.Should().NotBeNullOrWhiteSpace();
        refreshedToken.RefreshToken.Should().NotBeNullOrWhiteSpace();

        _tokenDto = refreshedToken;
    }

    [Fact(DisplayName = "04 - Revoke Token"), TestPriority(4)]
    public async Task TestRevokeToken()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tokenDto!.AccessToken);

        // Act
        var response = await _client.PostAsync("/auth/revoke", null);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact(DisplayName = "05 - Refresh with Revoked Token"), TestPriority(5)]
    public async Task TestSignInWithRevokedToken()
    {
        // Arrange
        var tokenDto = _tokenDto;

        // Act
        var response = await _client.PostAsJsonAsync("/auth/refresh", tokenDto);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
    }

    [Fact(DisplayName = "06 - Sign In with Invalid Credentials"), TestPriority(6)]
    public async Task TestSignInWithInvalidCredentials()
    {
        // Arrange
        var userDto = new UserDto
        {
            UserName = _accountCredentialsDto!.UserName,
            Password = "wrongpassword"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/auth/signin", userDto);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
    }
}