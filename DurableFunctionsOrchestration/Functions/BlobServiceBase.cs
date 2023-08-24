using System;
using DurableFunctionsOrchestration.Models;
using DurableFunctionsOrchestration.Services;

namespace DurableFunctionsOrchestration.Functions;

public abstract class BlobServiceBase
{
    private readonly IContainerService _containerService;
    private readonly string _containerName;

    protected BlobServiceBase(IContainerService containerService, string containerName)
    {
        _containerService = containerService ?? throw new ArgumentNullException(nameof(containerService));
        _containerName = containerName;
    }

    protected ContainerResponseResult Execute()
    {
        var blobResult = _containerService.GetBlobs(_containerName);

        var result = new ContainerResponseResult();

        foreach (var blob in blobResult)
        {
            result.NomeDocumentos.Add(blob.Name);
        }

        return result;
    }
}