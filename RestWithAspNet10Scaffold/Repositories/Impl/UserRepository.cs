using RestWithAspNet10Scaffold.Contexts;
using RestWithAspNet10Scaffold.Models;

namespace RestWithAspNet10Scaffold.Repositories.Impl;

public class UserRepository(MSSQLContext mssqlContext) : GenericRepository<User>(mssqlContext), IUserRepository
{
    public User? FindByUserName(string userName)
    {
        return _mssqlContext.Users.SingleOrDefault(u => u.UserName == userName);
    }
}