using RestWithAspNet10Scaffold.Data.Dto;
using RestWithAspNet10Scaffold.Data.Dto.V1;

namespace RestWithAspNet10Scaffold.Services;

public interface IPersonServices
{
    PersonDto Create(PersonDto person);
    PersonDto? FindById(long id);
    List<PersonDto> FindAll();
    PersonDto? Update(PersonDto person);
    void Delete(long id);
    PersonDto? Disable(long id);
}