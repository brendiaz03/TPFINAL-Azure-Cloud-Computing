using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.Logica.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> SubirArchivoAsync(IFormFile archivo, string nombreContenedor);
        string CleanFileName(string fileName);
        Task<string> ObtenerDuracionDelArchivo(string containerName, string audioBlobUrl);

    }
}
