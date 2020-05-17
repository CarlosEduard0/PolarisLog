namespace PolarisLog.WebApi.Payloads
{
    public class CadastrarUsuarioPayload
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string SenhaConfirmacao { get; set; }
    }
}