namespace ThirdEye.Console.Infrastructure
{
    public class AppSettings
    {
        public CognitiveServices CognitiveServices { get; set; }
        public Secrets Secrets { get; set; }
    }

    public class CognitiveServices
    {
        public string Storage { get; set; }
    }

    public class Secrets
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
