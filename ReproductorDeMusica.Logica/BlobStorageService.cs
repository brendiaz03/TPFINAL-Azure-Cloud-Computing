using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using NAudio.Wave;
using ReproductorDeMusica.Logica.Interfaces;
using System.IO;
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
            // Verifica que el archivo no sea nulo y que contenga datos
            if (archivo == null || archivo.Length == 0)
            {
                throw new ArgumentException("El archivo no puede ser nulo o vacío.");
            }

            // Limpia el nombre del archivo
            string cleanFileName = CleanFileName(archivo.FileName);
            // Obtener el cliente del contenedor
            var containerClient = _blobServiceClient.GetBlobContainerClient(nombreContenedor);
            await containerClient.CreateIfNotExistsAsync();

            // Generar un nombre único para el archivo
            var uniqueBlobName = $"{Path.GetFileNameWithoutExtension(cleanFileName)}_{Guid.NewGuid()}{Path.GetExtension(cleanFileName)}";
            var blobClient = containerClient.GetBlobClient(uniqueBlobName);

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
                    ExpiresOn = DateTime.UtcNow.AddDays(30)
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
        public async Task<string> ObtenerDuracionDelArchivo(string containerName, string audioBlobUrl)
        {

            // Obtener el cliente del contenedor y del blob
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            string baseUrl = "https://pw3storage.blob.core.windows.net/audios/";

            string uri = audioBlobUrl.Replace(baseUrl, "");
            int mp3Index = uri.LastIndexOf("mp3");
            uri = uri.Substring(0, mp3Index + 3);  // Incluye "mp3"
            var blobClient = containerClient.GetBlobClient(uri);

            // Descargar el contenido del blob a un MemoryStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await blobClient.DownloadToAsync(memoryStream);

                // Reestablecer la posición del MemoryStream a 0 para leer desde el inicio
                memoryStream.Position = 0;

                // Usamos NAudio para leer la duración del archivo MP3 desde el MemoryStream
                using (var mp3Reader = new Mp3FileReader(memoryStream))
                {
                    TimeSpan duration = mp3Reader.TotalTime;

                    // Formateamos la duración en formato MM:SS
                    return string.Format("{0:D2}:{1:D2}", duration.Minutes, duration.Seconds);
                }
            }

        }

    }

}
