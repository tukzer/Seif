namespace Seif.Soa.Transport
{
    public abstract class AbstractResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}