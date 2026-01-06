using RestWithAspNet10Scaffold.Data.Convert.Contract;
using RestWithAspNet10Scaffold.Data.Dto;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Data.Convert.Impl;

public class PersonConverter : IParser<Person, PersonDto>, IParser<PersonDto, Person>
{
    public Person Parser(PersonDto origin)
    {
        if (origin == null) return null;

        return new Person
        {
            Id = origin.Id,
            FirstName = origin.FirstName,
            LastName = origin.LastName,
            Address = origin.Address,
            Gender = origin.Gender,
            Enabled = origin.Enabled
        };
    }
    
    public List<Person> ParserList(List<PersonDto> origins)
    {
        if (origins.Count == 0) return null;
        return  origins.Select(o => Parser(o)).ToList();
    }

    public PersonDto Parser(Person? origin)
    {
        if (origin == null) return null;
        
        return new PersonDto
        {
            Id = origin.Id,
            FirstName = origin.FirstName,
            LastName = origin.LastName,
            Address = origin.Address,
            Gender = origin.Gender,
            Enabled = origin.Enabled
        };
    }

    public List<PersonDto> ParserList(List<Person> origins)
    {
        if (origins.Count == 0) return null;
        return  origins.Select(o => Parser(o)).ToList();
    }
}