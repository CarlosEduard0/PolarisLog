namespace PolarisLog.WebApi.Payloads.Log
{
    public class LogQueryPayload : QueryPayload
    {
        public string Origem { get; set; }
        public string Descricao { get; set; }
    }
}