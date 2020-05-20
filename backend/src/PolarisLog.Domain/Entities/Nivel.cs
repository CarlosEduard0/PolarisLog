namespace PolarisLog.Domain.Entities
{
    public class Nivel : Entity
    {
        public string Nome { get; set; }

        public Nivel(string nome)
        {
            Nome = nome;
        }
    }
}