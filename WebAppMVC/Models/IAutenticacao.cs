namespace WebAppMVC.Models
{
    public interface IAutenticacao
    {
        string GetConnectionString();
        string RegistrarUsuario(Usuario usuario);
        string ValidarLogin(Usuario usuario);
    }
}
