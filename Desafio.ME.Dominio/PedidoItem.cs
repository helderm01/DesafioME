namespace Desafio.ME.Dominio
{
    public class PedidoItem : EntidadeBase
    {
        const uint TAMANHO_DESCRICAO = 50;

        protected PedidoItem() { }

        public PedidoItem(string descricao, uint qtde, decimal preco)
        {
            DefinirDescricao(descricao);
            DefinirPrecoUnitario(preco);
            DefinirQuantidade(qtde);
        }

        public string Descricao { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public uint Quantidade { get; private set; }
        public decimal ValorTotal => PrecoUnitario * Quantidade;
        
        public void DefinirDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição é obrigatória.");

            if (descricao.Length > TAMANHO_DESCRICAO)
                throw new ArgumentException($"Descrição deve conter no máximo {TAMANHO_DESCRICAO} caracteres.");

            Descricao = descricao;
        }
        
        public void DefinirQuantidade(uint qtd)
        {
            if (qtd == 0)
                throw new ArgumentException("Quantidade deve ser maior que 0.");

            Quantidade = qtd;
        }

        public void DefinirPrecoUnitario(decimal preco)
        {
            if (preco < 0)
                throw new ArgumentException("Preço unitário não pode ser menor que 0.");

            PrecoUnitario = preco;
        }
    }
}
