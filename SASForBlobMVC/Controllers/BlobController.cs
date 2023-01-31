using Azure.Storage.Blobs;
using SASForBlobMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SASForBlobMVC.Controllers
{
    public class BlobController : Controller
    {
        // GET: Blob SAS url

        private readonly BlobSAS _blobSAS;
        public BlobController(BlobSAS blobSAS)
        {
            this._blobSAS = blobSAS;
        }

        BlobContainerClient blobContainerClient = CreateContainer("con1", false);

        private static BlobServiceClient blobServiceClient;
        private static BlobContainerClient CreateContainer(string containerName, bool isPublic)
        {
            //code for creating container
            blobServiceClient = new BlobServiceClient(ConfigurationManager.AppSettings["blobconstr"]);
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

            if (!blobContainerClient.Exists())
            {
                blobContainerClient.CreateIfNotExists();
                Console.WriteLine("container created container_name");
                if (isPublic)
                {
                    blobContainerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
                }
            }
            return blobContainerClient;
        }

        public ActionResult Index()
        {
            CreateContainer("conatiner_name", true);
            _blobSAS.CreateSASforBlob(blobContainerClient.GetBlobClient("blob_name"));
            return View();
        }
    }
}