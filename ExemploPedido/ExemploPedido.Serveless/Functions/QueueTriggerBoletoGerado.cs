using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Azure.ServiceBus;
using System.Text;
using ExemploPedido.Serveless.Dominio.Eventos;

namespace ExemploPedido.Serveless.Functions
{
    public class QueueTriggerBoletoGerado
    {
        public QueueTriggerBoletoGerado() { }

        [FunctionName(nameof(QueueTriggerBoletoGerado))]
        public async Task Run(
            [ServiceBusTrigger(nameof(BoletoGeradoEvento), Connection = "AzureServiceBus")] Message message,
            ILogger logger)
        {
            logger.LogInformation($"Gatilho QueueTriggerBoletoGerado recebido");
            var evento = Encoding.UTF8.GetString(message.Body).ToObject<BoletoGeradoEvento>();
            logger.LogTrace($"Enviando boleto por email {evento.NossoNumero} para pedido {evento.PedidoId}");
        }
    }
}
