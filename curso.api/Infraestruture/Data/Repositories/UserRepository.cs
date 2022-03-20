using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Model.Inputs;
using System.Linq;

namespace curso.api.Infraestruture.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CourseDbContext _context;
        public UserRepository(CourseDbContext context)
        {
            _context = context;
        }

        public void Register(User user)
        {
            _context.User.Add(user);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public User GetUser(string login)
        {
            return _context.User.FirstOrDefault(u => u.Login == login);
        }
    }
}
