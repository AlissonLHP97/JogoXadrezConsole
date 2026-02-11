using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);
                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0,0));
                tab.ColocarPeca(new Piao(tab, Cor.Preta), new Posicao(1, 1));
                tab.ColocarPeca(new Cavalo(tab, Cor.Preta), new Posicao(0, 2));

                tab.ColocarPeca(new Torre(tab, Cor.Branca), new Posicao(7, 0));
                tab.ColocarPeca(new Piao(tab, Cor.Branca), new Posicao(6, 1));
                tab.ColocarPeca(new Cavalo(tab, Cor.Branca), new Posicao(7, 2));

                Tela.imprimirTela(tab);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }






        }
    }
}

