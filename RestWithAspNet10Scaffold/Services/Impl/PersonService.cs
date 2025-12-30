using Mapster;
using RestWithAspNet10Scaffold.Data.Convert.Impl;
using RestWithAspNet10Scaffold.Data.Dto;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Hypermedia.Helpers;
using RestWithAspNet10Scaffold.Models;
using RestWithAspNet10Scaffold.Repositories;

namespace RestWithAspNet10Scaffold.Services.Impl;

public class PersonService : IPersonServices
{
    private IPersonRepository _personRepository;
    private readonly PersonConverter _personConverter;
    
    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
        _personConverter = new PersonConverter();
    }
    
    public PersonDto Create(PersonDto personDto)
    {
        Person person = _personConverter.Parser(personDto);
        return  _personConverter.Parser(_personRepository.Create(person));
    }

    public PersonDto? FindById(long id)
    {
        return _personConverter.Parser(_personRepository.FindById(id));
    }

    public List<PersonDto> FindAll()
    {
        return _personConverter.ParserList(_personRepository.FindAll());
    }

    public PersonDto? Update(PersonDto personDto)
    {
        Person person = _personConverter.Parser(personDto);
        return  _personConverter.Parser(_personRepository.Update(person));
    }

    public void Delete(long id)
    {
        var person = _personRepository.FindById(id);
        if (person == null) return;
        _personRepository.Delete(person);
    }
    
    public PersonDto? Disable(long id)
    {
        var person = _personRepository.Disable(id);
        return person.Adapt<PersonDto>();
    }
    
    public PersonDto? Enable(long id)
    {
        var person = _personRepository.Enable(id);
        return person.Adapt<PersonDto>();
    }

    public List<PersonDto> FindByName(string? firstName, string? lastName)
    {
        var persons = _personRepository.FindByName(firstName, lastName);
        return _personConverter.ParserList(persons);
    }

    public PagedSearchDto<PersonDto> FindWithPagedSearch(string? name, string sortDirection, int pageSize, int page)
    {
        var (query, countQuery, sort, size, offset) = BuildQueries(name, sortDirection, pageSize, page);
        
        var persons = _personRepository.FindWithPagedSearch(query);
        var totalResults = _personRepository.GetCount(countQuery);

        return new PagedSearchDto<PersonDto>
        {
            CurrentPage = page,
            List = persons.Adapt<List<PersonDto>>(),
            PageSize = size,
            SortDirection = sort,
            TotalResults = totalResults
        };
    }

    private (string query, string countQuery, string sort, int size, int offset) BuildQueries(string? name, string sortDirection, int pageSize, int page)
    {
        page = Math.Max(1, page);
        
        var offset = (page - 1) * pageSize;
        var size = pageSize < 1 ? 1 : pageSize;
        
        var sort = (!string.IsNullOrWhiteSpace(sortDirection) && sortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase)) ? "desc" : "asc";
        
        var baseQuery = $@"FROM Person p WHERE 1=1 ";
        
        if (!string.IsNullOrWhiteSpace(name)) baseQuery += $" AND (p.FirstName LIKE '%{name}%') ";
        
        var countQuery = $"SELECT COUNT(*) {baseQuery}";
        var query = $@"SELECT * {baseQuery} ORDER BY p.FirstName {sort} OFFSET {offset} ROWS FETCH NEXT {size} ROWS ONLY";
        
        return (query, countQuery, sort, size, offset);
    }
}