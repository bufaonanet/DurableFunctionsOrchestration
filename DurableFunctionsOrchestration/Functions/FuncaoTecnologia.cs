using System;
using System.Threading.Tasks;
using DurableFunctionsOrchestration.Models;
using DurableFunctionsOrchestration.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace DurableFunctionsOrchestration.Functions;

public class FuncaoTecnologia : BlobServiceBase
{
    public FuncaoTecnologia(IContainerService containerService)
        : base(containerService, Global.CONTAINER_TECNOLOGIA)
    {
    }

    [FunctionName(nameof(FuncaoTecnologia))]
    public Task<ContainerResponseResult> Run([ActivityTrigger] string name)
    {
        var result = Execute();

        return Task.FromResult(result);
    }
}