using System;
using System.Threading.Tasks;
using DurableFunctionsOrchestration.Models;
using DurableFunctionsOrchestration.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableFunctionsOrchestration.Functions;

public class FuncaoComercial : BlobServiceBase
{
    public FuncaoComercial(IContainerService containerService)
        : base(containerService, Global.CONTAINER_COMERCIAL)
    {
    }

    [FunctionName(nameof(FuncaoComercial))]
    public Task<ContainerResponseResult> Run([ActivityTrigger] string name)
    {
        var result = Execute();

        return Task.FromResult(result);
    }
}