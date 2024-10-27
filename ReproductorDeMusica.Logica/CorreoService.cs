using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ReproductorDeMusica.Logica.Interfaces;

namespace ReproductorDeMusica.Logica
{
    public class CorreoService : ICorreoService
    {

        private readonly HttpClient _httpCliente;
        public CorreoService(HttpClient httpCliente)
        {
            _httpCliente = httpCliente;
        }

        //Llama a la azure function (demo)
        public async Task EnviarCorreoPago(string toEmail)
        {
            string subject = "Plan aprobado";
            string content = "Su plan a adquirir fue hecho";
            string url = $"http://localhost:7071/api/AzFunEnviarCorreo?subject={subject}&content={content}&to={toEmail}";
            await _httpCliente.GetAsync(url);
        }

    }
}
