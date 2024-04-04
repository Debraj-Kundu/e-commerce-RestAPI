using UserAuthService.Domain;

namespace UserAuthService.Infrastructure
{
    public class UsersData
    {
        public List<UserDomain> Users { get; set; }
        public UsersData() 
        {
            Users = new List<UserDomain>()
            {
                new UserDomain()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Test1",
                    Email = "Test1@test.com",
                    Password = "Test@123",
                    Role = "admin"
                },
                new UserDomain()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Test2",
                    Email = "Test2@test.com",
                    Password = "Test@123",
                    Role = "customer"
                },
                new UserDomain()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Test3",
                    Email = "Test3@test.com",
                    Password = "Test@123",
                    Role = "customer"
                },
                new UserDomain()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Test4",
                    Email = "Test4@test.com",
                    Password = "Test@123",
                    Role = "admin"
                }
            };
        }
    }
}
