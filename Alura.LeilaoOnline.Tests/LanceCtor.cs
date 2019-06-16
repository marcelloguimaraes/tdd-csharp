using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void Dado_LanceComValorNegativo_Entao_RetornaArgumentException()
        {
            //Arrange
            int valorNegativo = -100;

            //Act/Assert
            Assert.Throws<ArgumentException>(() => new Lance(null, valorNegativo));
        }
    }
}
