namespace ExemploPedido.Serveless.Dominio.Eventos
{
    public sealed class PagamentoAprovadoEvento
    {
        public PagamentoAprovadoEvento(string pedidoId)
        {
            PedidoId = pedidoId;
        }

        public string PedidoId { get; set; }
    }
}
