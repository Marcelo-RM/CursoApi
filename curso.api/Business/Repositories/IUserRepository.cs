using curso.api.Business.Entities;
using curso.api.Model.Inputs;

namespace curso.api.Business.Repositories
{
    public interface IUserRepository
    {
        void Register(User user);
        void Commit();
        User GetUser(string login);
    }
}
