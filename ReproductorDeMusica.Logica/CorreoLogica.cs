using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Logica
{

    public interface ICorreoLogica{
        Task EnviarCorreoPago(string toEmail);
    }
    public class CorreoLogica : ICorreoLogica
    {

        private readonly HttpClient _cliente;
        public CorreoLogica(HttpClient cliente)
        {
            _cliente = cliente;
        }

        //Llama a la azure function (demo)
        public async Task EnviarCorreoPago(string toEmail)
        {
            string subject = "Plan aprobado";
            string content = "Su plan a adquirir fue hecho";
            string url = $"http://localhost:7065/api/AzFunEnviarCorreo?subject={subject}&content={content}&to={toEmail}";
            await _cliente.GetAsync(url);
        }

    }
}
