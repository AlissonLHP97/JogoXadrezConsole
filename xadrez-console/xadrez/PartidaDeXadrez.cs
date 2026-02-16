using System;
using System.Security.Cryptography.X509Certificates;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            colocarPecas();
        }
        public void executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimento();
            Peca capturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
        }
        public void realizarJogada(Posicao origem, Posicao destino)
        {
            executarMovimento(origem, destino);
            turno++;
            mudarJogador();


        }
        public void validarPosicaoOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Naão exite peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem não é sua!");
            }
            if (!tab.peca(pos).existeMovimentoPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peça de origem escolhida!");
            }
        }
        public void validarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if(!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino Inválida!");
            }
        }
        private void mudarJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }                     
        private void colocarPecas()
        {
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('a', 1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('h', 1).toPosicao());
            //tab.colocarPeca(new Cavalo(tab, Cor.Branca), new PosicaoXadrez('b', 1).toPosicao());
            //tab.colocarPeca(new Cavalo(tab, Cor.Branca), new PosicaoXadrez('g', 1).toPosicao());
            //tab.colocarPeca(new Bispo(tab, Cor.Branca), new PosicaoXadrez('c', 1).toPosicao());
            //tab.colocarPeca(new Bispo(tab, Cor.Branca), new PosicaoXadrez('f', 1).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('e', 1).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('c', 1).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 2).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('c', 2).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('e', 2).toPosicao());
            //tab.colocarPeca(new Rainha(tab, Cor.Branca), new PosicaoXadrez('e', 1).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('a', 2).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('b', 2).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('c', 2).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('d', 2).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('e', 2).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('f', 2).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('g', 2).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('h', 2).toPosicao());

            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('a', 8).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('h', 8).toPosicao());
            //tab.colocarPeca(new Cavalo(tab, Cor.Preta), new PosicaoXadrez('b', 8).toPosicao());
            //tab.colocarPeca(new Cavalo(tab, Cor.Preta), new PosicaoXadrez('g', 8).toPosicao());
            //tab.colocarPeca(new Bispo(tab, Cor.Preta), new PosicaoXadrez('c', 8).toPosicao());
            //tab.colocarPeca(new Bispo(tab, Cor.Preta), new PosicaoXadrez('f', 8).toPosicao());
            // tab.colocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('d', 8).toPosicao());
            //tab.colocarPeca(new Rainha(tab, Cor.Preta), new PosicaoXadrez('e', 8).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Preta), new PosicaoXadrez('a', 7).toPosicao());
            // tab.colocarPeca(new Piao(tab, Cor.Preta), new PosicaoXadrez('b', 7).toPosicao());
            // tab.colocarPeca(new Piao(tab, Cor.Preta), new PosicaoXadrez('c', 7).toPosicao());
            //  tab.colocarPeca(new Piao(tab, Cor.Preta), new PosicaoXadrez('d', 7).toPosicao());
            // tab.colocarPeca(new Piao(tab, Cor.Preta), new PosicaoXadrez('e', 7).toPosicao());
            //  tab.colocarPeca(new Piao(tab, Cor.Preta), new PosicaoXadrez('f', 7).toPosicao());
            //  tab.colocarPeca(new Piao(tab, Cor.Preta), new PosicaoXadrez('g', 7).toPosicao());
            // tab.colocarPeca(new Piao(tab, Cor.Preta), new PosicaoXadrez('h', 7).toPosicao());
        }


    }
}
