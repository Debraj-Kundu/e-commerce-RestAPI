using UserAuthService.Domain;

namespace UserAuthService.Infrastructure
{
    public interface IUserRepository
    {
        IEnumerable<UserDomain> GetAll();
        UserDomain GetById(string id);
        void Add(UserDomain user);
        void Update(string id, UserDomain user);
        void Delete(string id);
        UserDomain Login(string email, string password);
    }
}
