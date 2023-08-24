using System.Collections.Generic;

namespace DurableFunctionsOrchestration.Models;

public class ContainerResponseResult
{
    public List<string> NomeDocumentos { get; set; } = new();
}