using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Data.Dto;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Hypermedia.Helpers;

namespace RestWithAspNet10Scaffold.Services;

public interface IPersonServices
{
    PersonDto Create(PersonDto person);
    PersonDto? FindById(long id);
    List<PersonDto> FindAll();
    PersonDto? Update(PersonDto person);
    void Delete(long id);
    PersonDto? Disable(long id);
    PersonDto? Enable(long id);
    List<PersonDto> FindByName(string? firstName, string? lastName);
    PagedSearchDto<PersonDto> FindWithPagedSearch(string? name, string sortDirection, int pageSize, int page);
    Task<List<PersonDto>> MassCreateAsync(IFormFile? file);
    FileContentResult ExportPagedSearch(string? name, string sortDirection, int pageSize, int page, string acceptHeader);
}