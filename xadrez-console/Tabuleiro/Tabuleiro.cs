namespace tabuleiro
{
    class Tabuleiro
    {
        public int linhas {  get; set; }
        public int colunas { get; set; }
        private Posicao[,] posicao;

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            posicao = new Posicao[linhas, colunas];
        }
    }
}
