using Alura.LeilaoOnline.Core;
using Alura.LeilaoOnline.Core.Interfaces;
using System;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Fact]
        public void Dado_LeilaoRecebeSegundoLanceDeMesmoUltimoInteressado_Entao_NaoPermiteReceberOutroLance()
        {
            // Arrange
            IModalidadeAvaliacao modalidade = new MaiorValor();
            Leilao leilao = new Leilao("Van Gogh", modalidade);
            Interessado fulano = new Interessado("Fulano", leilao);

            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 1000);

            // Act

            leilao.RecebeLance(fulano, 1200);


            int valorEsperado = 1;
            int valorObtido = leilao.Lances.Count();

            //Assert
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData(2, new double[] { 800, 900 })]
        public void Dado_LeilaoFinalizado_Entao_NaoPermiteNovosLances(double qtdEsperada, double[] ofertas)
        {
            // Arrange
            IModalidadeAvaliacao modalidade = new MaiorValor();
            Leilao leilao = new Leilao("Van Gogh", modalidade);
            Interessado fulano = new Interessado("Fulano", leilao);
            Interessado maria = new Interessado("Maria", leilao);


            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                double valor = ofertas[i];

                if (i % 2 == 0)
                    leilao.RecebeLance(fulano, valor);
                else
                    leilao.RecebeLance(maria, valor);
            }

            leilao.TerminaPregao();

            leilao.RecebeLance(fulano, 1000);

            // Act

            double valorEsperado = qtdEsperada;
            int valorObtido = leilao.Lances.Count();

            //Assert
            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
