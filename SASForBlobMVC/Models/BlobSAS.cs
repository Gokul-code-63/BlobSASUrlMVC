using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SASForBlobMVC.Models
{
    public class BlobSAS
    {
        public string CreateSASforBlob(BlobClient blobClient, string storedPolicyName = null)
        {
            if (blobClient.CanGenerateSasUri)
            {
                BlobSasBuilder blobSasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = blobClient.BlobContainerName,
                    BlobName = blobClient.Name,
                    Resource = "b"
                };
                if (storedPolicyName == null)
                {
                    blobSasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
                    blobSasBuilder.SetPermissions(BlobSasPermissions.Read | BlobSasPermissions.Read);
                }
                else
                {
                    blobSasBuilder.Identifier = storedPolicyName;
                }
                Uri sasUri = blobClient.GenerateSasUri(blobSasBuilder);
                Console.WriteLine("Blob SAS uri" + sasUri);
                Console.ReadKey();
                return sasUri.ToString();
               
            }

            return "SAS url not generated";
            
        }
    }
}