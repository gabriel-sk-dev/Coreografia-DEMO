using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Azure.ServiceBus;
using System.Text;
using ExemploPedido.Serveless.Dominio.Eventos;

namespace ExemploPedido.Serveless.Functions
{
    public class QueueTriggerPrepararParaEnvio
    {
        public QueueTriggerPrepararParaEnvio() { }

        [FunctionName(nameof(QueueTriggerPrepararParaEnvio))]
        public async Task Run(
            [ServiceBusTrigger(nameof(PagamentoAprovadoEvento), Connection = "AzureServiceBus")] Message message,
            ILogger logger)
        {
            logger.LogInformation($"Gatilho QueueTriggerPrepararParaEnvio recebido");
            var comando = Encoding.UTF8.GetString(message.Body).ToObject<PagamentoAprovadoEvento>();

        }
    }
}
