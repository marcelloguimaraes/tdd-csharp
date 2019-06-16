using System;

namespace Alura.LeilaoOnline.Core
{
    public class Lance : IComparable
    {
        public Interessado Cliente { get; }
        public double Valor { get; }

        public Lance(Interessado cliente, double valor)
        {
            if (valor < 0)
                throw new ArgumentException($"{nameof(valor)} não pode ser negativo");

            Cliente = cliente;
            Valor = valor;
        }

        public int CompareTo(object that)
        {
            var lance = that as Lance;
            return Valor.CompareTo(lance.Valor);
        }
    }
}