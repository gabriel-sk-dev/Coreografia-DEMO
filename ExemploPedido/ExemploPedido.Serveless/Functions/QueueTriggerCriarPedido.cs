using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ExemploPedido.Serveless.Dominio.Comandos;
using Microsoft.Azure.WebJobs.ServiceBus;
using System;
using Microsoft.Azure.ServiceBus;
using System.Text;
using ExemploPedido.Serveless.Dominio;
using ExemploPedido.Serveless.Dominio.Eventos;

namespace ExemploPedido.Serveless.Functions
{
    public class QueueTriggerCriarPedido
    {
        private readonly CriarNovoPedidoServico _servico;

        public QueueTriggerCriarPedido(CriarNovoPedidoServico servico)
        {
            _servico = servico;
        }

        [FunctionName(nameof(QueueTriggerCriarPedido))]
        public async Task Run(
            [ServiceBusTrigger(nameof(NovoPedidoComando), Connection = "AzureServiceBus")] Message message,
            [ServiceBus(nameof(PedidoCriadoEvento), Connection = "AzureServiceBus", EntityType = EntityType.Queue)] IAsyncCollector<PedidoCriadoEvento> collector,
            ILogger logger)
        {
            logger.LogInformation($"Gatilho QueueTriggerCriarPedido recebido");
            var comando = Encoding.UTF8.GetString(message.Body).ToObject<NovoPedidoComando>();
            var resultado = await _servico.Executar(comando);
            var evento = new PedidoCriadoEvento(resultado.Id, comando.FormaPagamento.Forma, comando.FormaPagamento.Token);
            await collector.AddAsync(evento);
        }
    }
}
