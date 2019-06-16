namespace Alura.LeilaoOnline.Core
{
    public class Interessado
    {
        public string Nome { get; }
        public Leilao Leilao { get; }

        public Interessado(string nome, Leilao leilao)
        {
            Nome = nome;
            Leilao = leilao;
        }
    }
}