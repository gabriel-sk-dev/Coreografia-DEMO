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
    public class QueueTriggerProcessarPagamentoCartao
    {
        private readonly PagamentoComCartaoServico _servico;

        public QueueTriggerProcessarPagamentoCartao(PagamentoComCartaoServico servico)
        {
            _servico = servico;
        }

        [FunctionName(nameof(QueueTriggerProcessarPagamentoCartao))]
        public async Task Run(
            [ServiceBusTrigger(nameof(PagarComCartaoComando), Connection = "AzureServiceBus")] Message message,
            [ServiceBus(nameof(PagamentoAprovadoEvento), Connection = "AzureServiceBus", EntityType = EntityType.Queue)] IAsyncCollector<PagamentoAprovadoEvento> filaPagamentoAprovado,
            [ServiceBus(nameof(PagamentoNegadoEvento), Connection = "AzureServiceBus", EntityType = EntityType.Queue)] IAsyncCollector<PagamentoNegadoEvento> filaPagamentoNegado,
            ILogger logger)
        {
            logger.LogInformation($"Gatilho QueueTriggerProcessarPagamentoCartao recebido");
            var comando = Encoding.UTF8.GetString(message.Body).ToObject<PagarComCartaoComando>();
            var resultado = await _servico.Executar(comando);
            if (resultado.Status == Transacao.EStatus.Aprovado)
                await filaPagamentoAprovado.AddAsync(new PagamentoAprovadoEvento(comando.PedidoId));
            else if(resultado.Status == Transacao.EStatus.Negado)
                await filaPagamentoNegado.AddAsync(new PagamentoNegadoEvento(comando.PedidoId));
        }
    }
}
