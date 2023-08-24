using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DurableFunctionsOrchestration.Functions;
using DurableFunctionsOrchestration.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace DurableFunctionsOrchestration;

public class DurableFunctionsOrchestration
{
    [FunctionName("StartDurableFunctions")]
    public async Task<HttpResponseMessage> HttpStart(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
        HttpRequestMessage req,
        [DurableClient] IDurableOrchestrationClient starter,
        ILogger log)
    {
        // Function input comes from the request content.
        var instanceId = await starter.StartNewAsync("OrchestrationFunctions", null);

        log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

        return starter.CreateCheckStatusResponse(req, instanceId);
    }

    [FunctionName("OrchestrationFunctions")]
    public async Task RunOrchestrator([OrchestrationTrigger] IDurableOrchestrationContext context)
    {
        var tasks = new List<Task<ContainerResponseResult>>
        {
            context.CallActivityAsync<ContainerResponseResult>(nameof(FuncaoComercial), "FuncaoComercial"),
            context.CallActivityAsync<ContainerResponseResult>(nameof(FuncaoEngenharia), "FuncaoEngenharia"),
            context.CallActivityAsync<ContainerResponseResult>(nameof(FuncaoTecnologia), "FuncaoTecnologia")
        };

        await Task.WhenAll(tasks);

        var resultFromFunctions = tasks
            .Select(f => f.Result).ToList();

        await context.CallActivityAsync(nameof(FuncaoConclusao), resultFromFunctions);
    }
}