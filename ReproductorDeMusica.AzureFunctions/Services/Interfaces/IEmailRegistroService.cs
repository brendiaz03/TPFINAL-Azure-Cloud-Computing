using ReproductorDeMusica.AzureFunctions.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.AzureFunctions.Services.Interfaces
{
    public interface IEmailRegistroService
    {
        Task<EmailRegistro> GuardarEmailRegistro(EmailRegistro emailRegistro);
        List<EmailRegistro> ObtenerLosEmailsNoEnviados();
        void ActualizarEmailEsEnviado(EmailRegistro emailRegistro);

        Task EliminarEmailRegistroPorUsuarioPlanId(int idUsuarioPlan);
    }
}
