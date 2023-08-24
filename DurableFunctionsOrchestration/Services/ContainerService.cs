using System;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace DurableFunctionsOrchestration.Services;

public class ContainerService : IContainerService
{
    public Pageable<BlobItem> GetBlobs(string containerName)
    {
        try
        {
            var blobContainerClient = new BlobContainerClient(
                Environment.GetEnvironmentVariable("AzureWebJobsStorage"),
                containerName
            );

            var result = blobContainerClient.GetBlobs();
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}