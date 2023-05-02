namespace WebApplication5.Models
{
    public class Request
    {
        public Request()
        {
            ConversationID = string.Empty;
            MessageID = string.Empty;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConversationID { get; set; }
        public string MessageID { get; set; }

    }

    public class MessageRequest : Request
    {
        public string Message { get; set; }
        public int Priority { get; set; }
        public int MessageType { get; set; }
        public int ContentType { get; set; }
        public List<MetaRequest> MetaData { get; set; }
    }

    public class MetaRequest
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
