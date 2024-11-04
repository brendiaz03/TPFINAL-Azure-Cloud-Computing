using Azure.Storage.Blobs;
using ReproductorDeMusica.AzureFunctions.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductorDeMusica.AzureFunctions.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        BlobServiceClient _blobServiceClient;
        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> CargarTemplateDesdeBlobAsync(string containerClientString, string blobClientString)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerClientString);
            BlobClient blobClient = containerClient.GetBlobClient(blobClientString);

            //Obtengo la plantilla en un string
            using (MemoryStream ms = new MemoryStream())
            {
                await blobClient.DownloadToAsync(ms);
                ms.Position = 0; // Reiniciar la posición del stream
                return System.Text.Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
