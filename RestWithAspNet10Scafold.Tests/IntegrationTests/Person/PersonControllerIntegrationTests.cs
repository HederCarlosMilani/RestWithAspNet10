using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scafold.Tests.IntegrationTests.Tools;
using Xunit.Abstractions;

namespace RestWithAspNet10Scafold.Tests.IntegrationTests.Person;

[TestCaseOrderer("RestWithAspNet10Scafold.Tests.IntegrationTests.Tools.PriorityOrderer",
    "RestWithAspNet10Scafold.Tests")]
public class PersonControllerIntegrationTests : IClassFixture<SqlServerFixture>
{
    private readonly HttpClient _client;
    private static PersonDto _person;

    public PersonControllerIntegrationTests(SqlServerFixture sqlFixture)
    {
        var factory = new CustomWebApplicationFactory<Program>(sqlFixture.ConnectionString);
        _client = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("http://localhost")
            });
    }

    [Fact(DisplayName = "01 - Create Person"), TestPriority(1)]
    public async Task TestCreatePerson()
    {
        // Arrange
        var personToCreate = new PersonDto
        {
            FirstName = "Linus",
            LastName = "Torvalds",
            Address = "Helsinki - Finland",
            Gender = "Male",
            Enabled = true
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("/person", personToCreate);
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var createdPerson = await response.Content.ReadFromJsonAsync<PersonDto>();
        createdPerson.Should().NotBeNull();
        createdPerson!.Id.Should().BeGreaterThan(0);
        createdPerson.FirstName.Should().Be(personToCreate.FirstName);
        createdPerson.LastName.Should().Be(personToCreate.LastName);
        createdPerson.Address.Should().Be(personToCreate.Address);
        createdPerson.Gender.Should().Be(personToCreate.Gender);
        createdPerson.Enabled.Should().Be(personToCreate.Enabled);
        
        _person = createdPerson;
    }

    [Fact(DisplayName = "02 - Get Person by Id"), TestPriority(2)]
    public async Task TestGetPersonById()
    {
        // Act
        var response = await _client.GetAsync($"/person/{_person.Id}");

        // Assert
        response.EnsureSuccessStatusCode();

        var fetchedPerson = await response.Content.ReadFromJsonAsync<PersonDto>();
        fetchedPerson.Should().NotBeNull();
        fetchedPerson!.Id.Should().Be(_person.Id);
        fetchedPerson.FirstName.Should().Be(_person.FirstName);
    }

    [Fact(DisplayName = "03 - Get All Persons"), TestPriority(3)]
    public async Task TestGetAllPersons()
    {
        // Act
        var response = await _client.GetAsync("/person");

        // Assert
        response.EnsureSuccessStatusCode();

        var persons = await response.Content.ReadFromJsonAsync<List<PersonDto>>();
        persons.Should().NotBeNull();
        persons!.Count.Should().BeGreaterThan(0);

    }

    [Fact(DisplayName = "04 - Update Person"), TestPriority(4)]
    public async Task TestUpdatePerson()
    {
        // Arrange
        _person.LastName = "UpdatedLastName";  
        
        // Act
        var response = await _client.PutAsJsonAsync("/person", _person);
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var updatedPerson = await response.Content.ReadFromJsonAsync<PersonDto>();
        updatedPerson.Should().NotBeNull();
        updatedPerson!.LastName.Should().Be("UpdatedLastName");
    }
    
    [Fact(DisplayName = "05 - Disable Person"), TestPriority(5)]
    public async Task TestDisablePerson()
    {
        // Act
        var response = await _client.PatchAsync($"/person/disable/{_person.Id}", null);
        
        // Assert
        response.EnsureSuccessStatusCode();
        
        var disabledPerson = await response.Content.ReadFromJsonAsync<PersonDto>();
        disabledPerson.Should().NotBeNull();
        disabledPerson!.Enabled.Should().BeFalse();
    }

    [Fact(DisplayName = "06 - Enable Person"), TestPriority(6)]
    public async Task TestEnablePerson()
    {
        // Act
        var response = await _client.PatchAsync($"/person/enable/{_person.Id}", null);

        // Assert
        response.EnsureSuccessStatusCode();

        var enabledPerson = await response.Content.ReadFromJsonAsync<PersonDto>();
        enabledPerson.Should().NotBeNull();
        enabledPerson!.Enabled.Should().BeTrue();
    }

    [Fact(DisplayName = "07 - Delete Person"), TestPriority(7)]
    public async Task TestDeletePerson()
    {
        // Act
        var response = await _client.DeleteAsync($"/person/{_person.Id}");

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
    }
}