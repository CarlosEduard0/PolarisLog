namespace PolarisLog.Domain.Entities
{
    public class Ambiente : Entity
    {
        public string Nome { get; private set; }

        public Ambiente(string nome)
        {
            Nome = nome;
        }
    }
}