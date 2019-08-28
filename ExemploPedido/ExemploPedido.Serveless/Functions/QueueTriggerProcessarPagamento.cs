using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ExemploPedido.Serveless.Dominio.Comandos;
using Microsoft.Azure.WebJobs.ServiceBus;
using System;
using Microsoft.Azure.ServiceBus;
using System.Text;
using ExemploPedido.Serveless.Dominio.Eventos;

namespace ExemploPedido.Serveless.Functions
{
    public class QueueTriggerProcessarPagamento
    {
        public QueueTriggerProcessarPagamento() { }

        [FunctionName(nameof(QueueTriggerProcessarPagamento))]
        public async Task Run(
            [ServiceBusTrigger(nameof(PedidoCriadoEvento), Connection = "AzureServiceBus")] Message message,
            [ServiceBus(nameof(PagarComCartaoComando), Connection = "AzureServiceBus", EntityType = EntityType.Queue)] IAsyncCollector<PagarComCartaoComando> filaCartao,
            [ServiceBus(nameof(GerarBoletoComando), Connection = "AzureServiceBus", EntityType = EntityType.Queue)] IAsyncCollector<GerarBoletoComando> filaBoleto,
            ILogger logger)
        {
            logger.LogInformation($"Gatilho QueueTriggerProcessarPagamento recebido");
            var evento = Encoding.UTF8.GetString(message.Body).ToObject<PedidoCriadoEvento>();
            if (evento.FormaPagamento == "Cartao")
                await filaCartao.AddAsync(new PagarComCartaoComando(evento.PedidoId, evento.TokenCartao));
            else if(evento.FormaPagamento == "Boleto")
                await filaBoleto.AddAsync(new GerarBoletoComando(evento.PedidoId));
        }
    }
}
