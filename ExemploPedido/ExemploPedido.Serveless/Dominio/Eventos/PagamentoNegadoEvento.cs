namespace ExemploPedido.Serveless.Dominio.Eventos
{
    public sealed class PagamentoNegadoEvento
    {
        public PagamentoNegadoEvento(string pedidoId)
        {
            PedidoId = pedidoId;
        }

        public string PedidoId { get; set; }
    }
}
