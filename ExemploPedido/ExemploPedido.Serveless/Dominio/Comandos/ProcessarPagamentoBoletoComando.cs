namespace ExemploPedido.Serveless.Dominio.Comandos
{
    public sealed class ProcessarPagamentoBoletoComando
    {
        public ProcessarPagamentoBoletoComando(string nossoNumero, string pedidoId)
        {
            NossoNumero = nossoNumero;
            PedidoId = pedidoId;
        }

        public string PedidoId { get; set; }
        public string NossoNumero { get; set; }
    }
}
