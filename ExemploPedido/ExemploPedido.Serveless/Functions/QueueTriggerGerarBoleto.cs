using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ExemploPedido.Serveless.Dominio.Comandos;
using Microsoft.Azure.WebJobs.ServiceBus;
using System;
using Microsoft.Azure.ServiceBus;
using System.Text;
using ExemploPedido.Serveless.Dominio.Eventos;
using ExemploPedido.Serveless.Dominio;

namespace ExemploPedido.Serveless.Functions
{
    public class QueueTriggerGerarBoleto
    {
        private readonly GerarBoletoServico _servico;

        public QueueTriggerGerarBoleto(GerarBoletoServico servico)
        {
            _servico = servico;
        }

        [FunctionName(nameof(QueueTriggerGerarBoleto))]
        public async Task Run(
            [ServiceBusTrigger(nameof(GerarBoletoComando), Connection = "AzureServiceBus")] Message message,
            [ServiceBus(nameof(BoletoGeradoEvento), Connection = "AzureServiceBus", EntityType = EntityType.Queue)] IAsyncCollector<BoletoGeradoEvento> filaBoletoGerado,
            ILogger logger)
        {
            logger.LogInformation($"Gatilho QueueTriggerGerarBoleto recebido");
            var comando = Encoding.UTF8.GetString(message.Body).ToObject<GerarBoletoComando>();
            var resultado = await _servico.Executar(comando);
            await filaBoletoGerado.AddAsync(new BoletoGeradoEvento(comando.PedidoId, resultado.NossoNumero));
        }
    }
}
