using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            xeque = false;
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }
        public Peca executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimento();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }
        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQtdMovimento();
            if(pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
        }
        public void realizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCpapturada = executarMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCpapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
            if(estaEmXeque(adversario(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
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
            if (!tab.peca(origem).podeMoverPara(destino))
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
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        private Cor adversario(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }
        private Peca rei(Cor cor)
        {
            foreach(Peca x in pecasEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException($"Não tem reu da cor {cor} no tabuleiro!");
            }
            foreach (Peca x in pecasEmJogo(adversario(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas()
        {            
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));

            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));

            //tab.colocarPeca(new Cavalo(tab, Cor.Branca), new PosicaoXadrez('b', 1).toPosicao());
            //tab.colocarPeca(new Cavalo(tab, Cor.Branca), new PosicaoXadrez('g', 1).toPosicao());
            //tab.colocarPeca(new Bispo(tab, Cor.Branca), new PosicaoXadrez('c', 1).toPosicao());
            //tab.colocarPeca(new Bispo(tab, Cor.Branca), new PosicaoXadrez('f', 1).toPosicao());            
            //tab.colocarPeca(new Rainha(tab, Cor.Branca), new PosicaoXadrez('e', 1).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('a', 2).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('b', 2).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('c', 2).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('d', 2).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('e', 2).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('f', 2).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('g', 2).toPosicao());
            //tab.colocarPeca(new Piao(tab, Cor.Branca), new PosicaoXadrez('h', 2).toPosicao());


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
