namespace ExemploPedido.Serveless.Dominio.Eventos
{
    public sealed class BoletoGeradoEvento
    {
        public BoletoGeradoEvento(string pedidoId, string nossoNumero)
        {
            PedidoId = pedidoId;
            NossoNumero = nossoNumero;
        }

        public string PedidoId { get; set; }
        public string NossoNumero { get; set; }
    }
}
