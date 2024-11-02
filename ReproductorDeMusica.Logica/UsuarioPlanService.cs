using ReproductorDeMusica.Entidades.Entidades;
using ReproductorDeMusica.Entidades.Repositories.Interfaces;

namespace ReproductorDeMusica.Logica;

public class UsuarioPlanService : IUsuarioPlanService
{
    private readonly IUsuarioPlanRepository _usuarioPlanRepository;

    public UsuarioPlanService(IUsuarioPlanRepository usuarioPlanRepository)
    {
        _usuarioPlanRepository = usuarioPlanRepository;
    }

    public Usuario ObtenerUsuarioConPlan(int usuarioId)
    {
        try
        {
            return _usuarioPlanRepository.ObtenerUsuarioConPlan(usuarioId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

public interface IUsuarioPlanService
{
    Usuario ObtenerUsuarioConPlan(int usuarioId);
}