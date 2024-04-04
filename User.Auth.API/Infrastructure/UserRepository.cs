using UserAuthService.Domain;

namespace UserAuthService.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private List<UserDomain> Users { get; set; }
        public UserRepository(UsersData usersData) 
        {
            Users = usersData.Users;
        }

        public IEnumerable<UserDomain> GetAll()
        {
            return Users;
        }
        public UserDomain GetById(string id)
        {
            return Users.FirstOrDefault(u => u.Id.Equals(id));
        }
        public void Add(UserDomain user)
        {
            Users.Add(user);
        }

        public void Update(string id, UserDomain user)
        {
            int index = Users.FindIndex(u => u.Id.Equals(id));
            Users[index].Name = user.Name;
            Users[index].Email = user.Email;
            Users[index].Password = user.Password;
            Users[index].Role = user.Role;
        }

        public void Delete(string id)
        {
            var userToDelete = GetById(id);
            Users.Remove(userToDelete);
        }

        public UserDomain? Login(string email, string password)
        {
            var user = Users.Where(u => u.Email.Equals(email) && u.Password.Equals(password)).FirstOrDefault();
            return user;
        }
    }
}
