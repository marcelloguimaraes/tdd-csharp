using Alura.LeilaoOnline.Core;
using Alura.LeilaoOnline.Core.Interfaces;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[] { 900, 1000, 1200, 1250, 1400})]
        public void Dado_LeilaoNessaModalidade_Entao_RetornaValorSuperiorMaisProximo(
            double valorDestino, double valorEsperado, double[] ofertas)
        {
            // Arrange
            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessado("Fulano", leilao);
            var maria = new Interessado("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                double valor = ofertas[i];

                if (i % 2 == 0)
                    leilao.RecebeLance(fulano, valor);
                else
                    leilao.RecebeLance(maria, valor);
            }

            // Act
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.LanceGanhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200})]
        [InlineData(1000, new double[] { 800, 900, 1000, 990})]
        [InlineData(800, new double[] { 800 })]
        public void Dado_LeilaoComPeloMenosUmLance_Entao_RetornaMaiorValor(double valorEsperado, double[] ofertas)
        {
            // Arrange
            IModalidadeAvaliacao modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessado("Fulano", leilao);
            var maria = new Interessado("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                double valor = ofertas[i];

                if (i % 2 == 0)
                    leilao.RecebeLance(fulano, valor);
                else
                    leilao.RecebeLance(maria, valor);
            }

            // Act
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.LanceGanhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void Dado_PregaoNaoIniciado_Entao_LancaInvalidOperationException()
        {
            // Arrange
            IModalidadeAvaliacao modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessado("Fulano", leilao);

            // Act Assert
            var msgObtida = Assert.Throws<InvalidOperationException>(() => leilao.TerminaPregao());

            var msgEsperada = "Não é possível terminar um pregão não iniciado";
            Assert.Equal(expected: msgEsperada, actual: msgObtida.Message);
        }

        [Fact]
        public void Dado_LeilaoSemLances_Entao_RetornaZero()
        {
            // Arrange
            IModalidadeAvaliacao modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessado("Fulano", leilao);

            leilao.IniciaPregao();

            // Act
            leilao.TerminaPregao();

            var valorEsperado = 0;
            var valorObtido = leilao.LanceGanhador.Valor;

            //Assert
            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
