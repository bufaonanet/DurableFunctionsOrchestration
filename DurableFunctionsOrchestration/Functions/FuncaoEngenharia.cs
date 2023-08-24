using System.Threading.Tasks;
using DurableFunctionsOrchestration.Models;
using DurableFunctionsOrchestration.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace DurableFunctionsOrchestration.Functions;

public class FuncaoEngenharia : BlobServiceBase
{
    public FuncaoEngenharia(IContainerService containerService)
        : base(containerService, Global.CONTAINER_ENGENHARIA)
    {
    }

    [FunctionName(nameof(FuncaoEngenharia))]
    public Task<ContainerResponseResult> Run([ActivityTrigger] string name)
    {
        var result = Execute();

        return Task.FromResult(result);
    }
}