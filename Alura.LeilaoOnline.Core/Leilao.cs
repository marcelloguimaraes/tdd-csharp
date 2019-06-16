using Alura.LeilaoOnline.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class Leilao
    {
        private IModalidadeAvaliacao _modalidadeAvaliacao;
        public IList<Lance> Lances { get; }
        public string Peca { get; }
        public Lance LanceGanhador { get; private set; }
        public SituacaoLeilao Situacao { get; private set; }
        public Interessado UltimoCliente { get; private set; }

        public Leilao(string peca, IModalidadeAvaliacao modalidadeAvaliacao)
        {
            Peca = peca;
            Lances = new List<Lance>();
            Situacao = SituacaoLeilao.AntesDoPregao;
            _modalidadeAvaliacao = modalidadeAvaliacao;
        }

        public enum SituacaoLeilao
        {
            AntesDoPregao,
            EmAndamento,
            Finalizado
        }


        public void RecebeLance(Interessado cliente, double valor)
        {
            if (PodeReceberLance(cliente))
            {
                Lances.Add(new Lance(cliente, valor));
                UltimoCliente = cliente;
            }
        }

        private bool PodeReceberLance(Interessado cliente) =>
            Situacao == SituacaoLeilao.EmAndamento &&
            cliente != UltimoCliente;

        public void IniciaPregao()
        {
            Situacao = SituacaoLeilao.EmAndamento;
        }

        public void TerminaPregao()
        {
            if (Situacao != SituacaoLeilao.EmAndamento)
                throw new InvalidOperationException("Não é possível terminar um pregão não iniciado");

            LanceGanhador = _modalidadeAvaliacao.Avalia(this);
            Situacao = SituacaoLeilao.Finalizado;
        }
    }
}
