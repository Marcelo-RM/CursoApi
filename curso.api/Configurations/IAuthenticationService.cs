using curso.api.Business.Entities;

namespace curso.api.Configurations
{
    public interface IAuthenticationService
    {
        string GetToken(User usuarioOutput);
    }
}
