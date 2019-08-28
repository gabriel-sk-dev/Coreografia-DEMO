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
    public class QueueTriggerProcessarRecebimentoBoleto
    {
        private readonly RecimentoBoletoService _servico;

        public QueueTriggerProcessarRecebimentoBoleto(RecimentoBoletoService servico)
        {
            _servico = servico;
        }

        [FunctionName(nameof(QueueTriggerProcessarRecebimentoBoleto))]
        public async Task Run(
            [ServiceBusTrigger(nameof(ProcessarPagamentoBoletoComando), Connection = "AzureServiceBus")] Message message,
            [ServiceBus(nameof(PagamentoAprovadoEvento), Connection = "AzureServiceBus", EntityType = EntityType.Queue)] IAsyncCollector<PagamentoAprovadoEvento> filaPagamentoAprovado,
            ILogger logger)
        {
            logger.LogInformation($"Gatilho QueueTriggerProcessarRecebimentoBoleto recebido");
            var comando = Encoding.UTF8.GetString(message.Body).ToObject<ProcessarPagamentoBoletoComando>();
            var resultado = await _servico.Executar(comando);
            await filaPagamentoAprovado.AddAsync(new PagamentoAprovadoEvento(comando.PedidoId));
        }
    }
}
