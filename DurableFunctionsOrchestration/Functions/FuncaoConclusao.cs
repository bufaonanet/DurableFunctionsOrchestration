using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Data.Tables;
using DurableFunctionsOrchestration.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace DurableFunctionsOrchestration.Functions;

public class FuncaoConclusao
{
    [FunctionName(nameof(FuncaoConclusao))]
    public async Task Run([ActivityTrigger] IDurableActivityContext context)
    {
        var listContainerResults = context.GetInput<List<ContainerResponseResult>>();

        var tableClient = new TableClient(
            System.Environment.GetEnvironmentVariable("AzureWebJobsStorage"),
            "nomesArquivosAreas"
        );

        foreach (var containerResult in listContainerResults)
        {
            foreach (var nome in containerResult.NomeDocumentos)
            {
                var tableEntity = new TableEntity()
                {
                    {"PartitionKey", Guid.NewGuid().ToString()},
                    {"RowKey", Guid.NewGuid().ToString()},
                    {"NomeArquivo", nome}
                };
                
                await tableClient.AddEntityAsync(tableEntity);
            }
        }
    }
}