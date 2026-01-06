using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RestWithAspNet10Scafold.Tests.IntegrationTests.Tools;

namespace RestWithAspNet10Scafold.Tests.IntegrationTests;

public class OpenApiIntegrationTests : IClassFixture<SqlServerFixture>
{
    private readonly HttpClient _httpClient;
    
    public OpenApiIntegrationTests(SqlServerFixture sqlServerFixture)
    {
        var factory = new CustomWebApplicationFactory<Program>(sqlServerFixture.ConnectionString);
        _httpClient = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost")
            }
            );
    }

    [Fact]
    public async Task Get_SwaggerJson_ShouldReturnOk()
    {
        // Arrange
        var requestUrl = "/swagger/V1.0.0/swagger.json";
        
        // Act
        var response = await _httpClient.GetAsync(requestUrl);
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain("person/{id}");
        content.Should().Contain("book/{id}");
    }
    
    // Test swagger ui access
    [Fact]
    public async Task Get_SwaggerUI_ShouldReturnOk()
    {
        // Arrange
        var requestUrl = "/swagger-ui/index.html";

        // Act
        var response = await _httpClient.GetAsync(requestUrl);

        // Assert
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain("<div id=\"swagger-ui\">");
    }
    
    // Test Scalar OpenAPI access
    [Fact]
    public async Task Get_ScalarUi_ShouldReturnOk()
    {
        // Arrange
        var requestUrl = "/scalar/";
        
        // Act
        var response = await _httpClient.GetAsync(requestUrl);
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        content.Should().Contain("<title>RestWithAspNet10Scaffold</title>");
    }
}