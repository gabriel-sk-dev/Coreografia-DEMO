namespace ExemploPedido.Serveless.Dominio.Eventos
{
    public sealed class PedidoCriadoEvento
    {
        public PedidoCriadoEvento(string pedidoId, string formaPagamento, string tokenCartao)
        {
            PedidoId = pedidoId;
            FormaPagamento = formaPagamento;
            TokenCartao = tokenCartao;
        }

        public string PedidoId { get; set; }
        public string FormaPagamento { get; set; }
        public string TokenCartao { get; set; }
    }
}
