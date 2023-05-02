namespace WebApplication5.Models
{
    public class AuthSettings
    {
        public string grant_type { get; set; }
        public string audience { get; set; }
        public string url { get; set; }
        public string scope { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }
}
