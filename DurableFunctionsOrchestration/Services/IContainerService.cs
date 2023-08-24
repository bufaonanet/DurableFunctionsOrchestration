using Azure;
using Azure.Storage.Blobs.Models;

namespace DurableFunctionsOrchestration.Services;

public interface IContainerService
{
    Pageable<BlobItem> GetBlobs(string containerName);
}