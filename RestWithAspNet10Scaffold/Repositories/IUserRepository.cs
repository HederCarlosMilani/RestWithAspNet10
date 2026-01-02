using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Repositories;

public interface IUserRepository : IRepository<User>
{
    User? FindByUserName(string userName);
}