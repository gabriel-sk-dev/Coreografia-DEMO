using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ExemploPedido.Serveless.Dominio.Comandos;
using Microsoft.Azure.WebJobs.ServiceBus;
using System;

namespace ExemploPedido.Serveless.Functions
{
    public class HttpTriggerRealizarPedido
    {
        public HttpTriggerRealizarPedido() { }

        [FunctionName(nameof(HttpTriggerRealizarPedido))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Pedidos")] HttpRequest requisicao,
            [ServiceBus(nameof(NovoPedidoComando), Connection = "AzureServiceBus", EntityType = EntityType.Queue)] IAsyncCollector<NovoPedidoComando> collector,
            ILogger logger)
        {
            logger.LogInformation($"Gatilho TriggerRealizarPedido recebido");

            var json = await new StreamReader(requisicao.Body).ReadToEndAsync();
            var comando = JsonConvert.DeserializeObject<NovoPedidoComando>(json);
            comando.Id = Guid.NewGuid().ToString();
            await collector.AddAsync(comando);
            return new OkObjectResult(comando.Id);
        }
    }
}
