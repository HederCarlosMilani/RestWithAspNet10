using FluentAssertions;
using RestWithAspNet10Scaffold.Data.Convert.Impl;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scafold.Tests;

public class PersonConverterTests
{
    private readonly PersonConverter _personConverter;
    public PersonConverterTests()
    {
        _personConverter = new PersonConverter();
    }
    
    // PersonDTO to Person conversion tests
    [Fact]
    public void Parse_ShouldConvertPersonDtoToPerson()
    {
        // Arrange: Prepare a PersonDto object
        var personDto = new PersonDto
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Address = "123 Main St",
            Gender = "Male"
        };
        
        // Act: Convert PersonDto to Person
        var person = _personConverter.Parser(personDto);
        
        // Assert: Verify the conversion
        Assert.NotNull(person);
        person.Should().NotBeNull();
        
        Assert.Equal(personDto.Id, person.Id);
        person.Id.Should().Be(personDto.Id);
        
        Assert.Equal(personDto.FirstName, person.FirstName);
        person.FirstName.Should().Be(personDto.FirstName);
        
        Assert.Equal(personDto.LastName, person.LastName);
        person.FirstName.Should().Be(personDto.FirstName);
        
        Assert.Equal(personDto.Address, person.Address);
        person.Address.Should().Be(personDto.Address);
        
        Assert.Equal(personDto.Gender, person.Gender);
        person.Gender.Should().Be(personDto.Gender);
        
        person.Should().BeEquivalentTo(personDto);
    }

    [Fact]
    public void Parse_ShouldReturnNull_WhenPersonDtoIsNull()
    {
        // Arrange: Prepare a null PersonDto
        PersonDto personDto = null;
        
        // Act: Convert null PersonDto to Person
        var person = _personConverter.Parser(personDto);
        
        // Assert: Verify the conversion result is null
        person.Should().BeNull();
    }
    
    // Person to PersonDTO conversion tests
    [Fact]
    public void Parse_ShouldConvertPersonToPersonDto()
    {
        // Arrange: Prepare a Person object
        var person = new Person
        {
            Id = 1,
            FirstName = "Jane",
            LastName = "Smith",
            Address = "456 Elm St",
            Gender = "Male"
        };
        
        // Act: Convert Person to PersonDto
        var personDto = _personConverter.Parser(person);
        
        // Assert: Verify the conversion
        personDto.Should().NotBeNull();
        personDto.Id.Should().Be(person.Id);
        personDto.FirstName.Should().Be(person.FirstName);
        personDto.LastName.Should().Be(person.LastName);
        personDto.Address.Should().Be(person.Address);
        personDto.Gender.Should().Be(person.Gender);
        
        personDto.Should().BeEquivalentTo(person);
    }

    [Fact]
    public void Parse_ShouldReturnNull_WhenPersonIsNull()
    {
        // Arrange: Prepare a null Person
        Person person = null;

        // Act: Convert null Person to PersonDto
        var personDto = _personConverter.Parser(person);

        // Assert: Verify the conversion result is null
        personDto.Should().BeNull();
    }
    
    // List PersonDTO to List Person conversion tests
    [Fact]
    public void ParseList_ShouldConvertPersonDtoListToPersonList()
    {
        // Arrange: Prepare a list of PersonDto objects with Moq
        var personDtos = new List<PersonDto>
        {
            new PersonDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main St",
                Gender = "Male"
            },
            new PersonDto
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Address = "456 Elm St",
                Gender = "Female"
            }
        };
        
        // Act: Convert List<PersonDto> to List<Person>
        var persons = _personConverter.ParserList(personDtos);
        
        // Assert: Verify the conversion
        persons.Should().NotBeNull();
        persons.Count.Should().Be(personDtos.Count);
        persons.Should().HaveCount(personDtos.Count);
        
        persons.Should().BeEquivalentTo(personDtos);

        for (int i = 0; i < personDtos.Count; i++)
        {
            persons[i].Should().NotBeNull();
            persons[i].Id.Should().Be(personDtos[i].Id);
            persons[i].FirstName.Should().Be(personDtos[i].FirstName);
            persons[i].LastName.Should().Be(personDtos[i].LastName);
            persons[i].Address.Should().Be(personDtos[i].Address);
            persons[i].Gender.Should().Be(personDtos[i].Gender);
            
            persons[i].Should().BeEquivalentTo(personDtos[i]);
        }
    }

    [Fact]
    public void ParseList_ShouldReturnNull_WhenPersonDtoListIsEmpty()
    {
        // Arrange: Prepare an empty list of PersonDto
        var personDtos = new List<PersonDto>();

        // Act: Convert empty List<PersonDto> to List<Person>
        var persons = _personConverter.ParserList(personDtos);

        // Assert: Verify the conversion result is null
        persons.Should().BeNull();
    }
    
    // List Person to List PersonDTO conversion tests
    [Fact]
    public void ParseList_ShouldConvertPersonListToPersonDtoList()
    {
        // Arrange: Prepare a list of Person objects
        var persons = new List<Person>
        {
            new Person()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 Main St",
                Gender = "Male"
            },
            new Person()
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Address = "456 Elm St",
                Gender = "Female"
            }
        };
        
        // Act: Convert List<Person> to List<PersonDto>
        var personDtos = _personConverter.ParserList(persons);
        
        // Assert: Verify the conversion
        personDtos.Should().NotBeNull();
        personDtos.Count.Should().Be(persons.Count);
        personDtos.Should().HaveCount(persons.Count);
        
        personDtos.Should().BeEquivalentTo(persons);
        for (int i = 0; i < persons.Count; i++)
        {
            personDtos[i].Should().NotBeNull();
            personDtos[i].Id.Should().Be(persons[i].Id);
            personDtos[i].FirstName.Should().Be(persons[i].FirstName);
            personDtos[i].LastName.Should().Be(persons[i].LastName);
            personDtos[i].Address.Should().Be(persons[i].Address);
            personDtos[i].Gender.Should().Be(persons[i].Gender);

            personDtos[i].Should().BeEquivalentTo(persons[i]);
        }
    }

    [Fact]
    public void ParseList_ShouldReturnNull_WhenPersonListIsEmpty()
    {
        // Arrange: Prepare an empty list of Person
        var persons = new List<Person>();

        // Act: Convert empty List<Person> to List<PersonDto>
        var personDtos = _personConverter.ParserList(persons);

        // Assert: Verify the conversion result is null
        personDtos.Should().BeNull();
    }
} 