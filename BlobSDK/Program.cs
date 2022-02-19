using Azure.Storage.Blobs;
using System;

namespace BlobSDK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=az204jansa;AccountKey=7QQnZY7yJS3Uk5clgYdeYINiXZyHuBzVYPGwffwiL1sHo21wCunpVlvUdRJJ7lnaVTEESkknxxSi+AStJNNFsA==;EndpointSuffix=core.windows.net";
            BlobServiceClient serviceclient = new BlobServiceClient(connectionString);


            BlobContainerClient containerClient = serviceclient.GetBlobContainerClient("az204-vs");
            containerClient.CreateIfNotExists();

            var fileName = "demo.txt";
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            blobClient.Upload(fileName);
        }
    }
}
