using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using ReproductorDeMusica.Logica.Interfaces;
using System.Text.RegularExpressions;

namespace ReproductorDeMusica.Logica
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public string CleanFileName(string fileName)
        {
            // Eliminar caracteres no válidos
            var cleanFileName = Path.GetFileName(fileName);
            cleanFileName = cleanFileName.Replace(" ", "_"); // Reemplaza espacios por guiones bajos
            cleanFileName = Regex.Replace(cleanFileName, @"[^a-zA-Z0-9\-_\.]", ""); // Elimina caracteres no válidos
            return cleanFileName.ToLower(); // Convertir a minúsculas
        }

        public async Task<string> SubirArchivoAsync(IFormFile archivo, string nombreContenedor)
        {
            // Obtener el cliente del contenedor
            var containerClient = _blobServiceClient.GetBlobContainerClient(nombreContenedor);
            await containerClient.CreateIfNotExistsAsync();

            // Generar un nombre único para el archivo
            var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName));

            // Subir el archivo usando el stream
            using (var stream = archivo.OpenReadStream())
            {
                await blobClient.UploadAsync(stream);
            }

            // Generar URL con SAS
            if (blobClient.CanGenerateSasUri)
            {
                BlobSasBuilder sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = containerClient.Name,
                    BlobName = blobClient.Name,
                    Resource = "b", 
                    ExpiresOn = DateTime.UtcNow.AddDays(30) //poner fecha de vencimiento alejada porque despues de esa fecha no se va a poder escuchr
                };

                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                Uri sasUri = blobClient.GenerateSasUri(sasBuilder);

                return sasUri.ToString();
            }
            else
            {
                throw new InvalidOperationException("No se pudo generar una URL SAS para este blob.");
            }
        }
    }

}
