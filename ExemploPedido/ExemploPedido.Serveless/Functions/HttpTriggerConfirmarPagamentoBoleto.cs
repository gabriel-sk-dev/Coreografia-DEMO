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
using ExemploPedido.Serveless.Dominio.Infra;

namespace ExemploPedido.Serveless.Functions
{
    public class HttpTriggerConfirmarPagamentoBoleto
    {
        private readonly EFContexto _contexto;

        public HttpTriggerConfirmarPagamentoBoleto(EFContexto contexto)
        {
            _contexto = contexto;
        }

        [FunctionName(nameof(HttpTriggerConfirmarPagamentoBoleto))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Boletos/{nossoNumero}")] HttpRequest requisicao,
            [ServiceBus(nameof(ProcessarPagamentoBoletoComando), Connection = "AzureServiceBus", EntityType = EntityType.Queue)] IAsyncCollector<ProcessarPagamentoBoletoComando> collector,
            string nossoNumero,
            ILogger logger)
        {
            logger.LogInformation($"Gatilho TriggerBoletoPago recebido");

            var boleto = await _contexto.Boletos.FindAsync(nossoNumero);
            if (boleto == null)
                return new NotFoundResult();

            var comando = new ProcessarPagamentoBoletoComando(boleto.NossoNumero, boleto.PedidoId);
            await collector.AddAsync(comando);

            return new OkResult();
        }
    }
}
