namespace PolarisLog.Domain.Entities
{
    public class Usuario : Entity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }

        protected Usuario()
        {
        }

        public Usuario(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }
    }
}