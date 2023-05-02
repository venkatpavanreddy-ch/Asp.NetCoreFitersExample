namespace WebApplication5.Models
{

    public class Conversations
    {
        public Value value { get; set; }
        public bool success { get; set; }
        public List<object> errors { get; set; }
    }

    public class Conversation
    {
        public string conversationId { get; set; }
        public int conversationType { get; set; }
        public DateTime createdDateTime { get; set; }
        public List<Member> members { get; set; }
        public LastMessage lastMessage { get; set; }
        public string title { get; set; }
        public object avatarInitials { get; set; }
        public int unreadMessageCount { get; set; }
    }

    public class DeliveryReceipts
    {
        public int low { get; set; }
        public int high { get; set; }
        public List<object> users { get; set; }
    }

    public class LastMessage
    {
        public string conversationMessageId { get; set; }
        public DateTime dateTime { get; set; }
        public string text { get; set; }
        public string fromUserId { get; set; }
        public string fromDisplayName { get; set; }
        public string fromAvatarInitials { get; set; }
        public int priority { get; set; }
        public int messageType { get; set; }
        public int status { get; set; }
        public DeliveryReceipts deliveryReceipts { get; set; }
        public ReadReceipts readReceipts { get; set; }
    }

    public class Member
    {
        public string userId { get; set; }
        public string displayName { get; set; }
        public List<string> roles { get; set; }
        public string avatarInitials { get; set; }
    }

    public class ReadReceipts
    {
        public int low { get; set; }
        public int high { get; set; }
        public List<object> users { get; set; }
    }

    public class Value
    {
        public List<Conversation> conversations { get; set; }
        public int totalResults { get; set; }
        public List<Message> messages { get; set; }
        public Message message { get; set; }

    }

    public class From
    {
        public User user { get; set; }
    }

    public class Message
    {
        public string conversationId { get; set; }
        public string conversationMessageId { get; set; }
        public DateTime createdDateTime { get; set; }
        public DateTime expirationDateTime { get; set; }
        public From from { get; set; }
        public string content { get; set; }
        public int contentType { get; set; }
        public int priority { get; set; }
        public object replyToMessageId { get; set; }
        public int status { get; set; }
        public ReadReceipts readReceipts { get; set; }
        public DeliveryReceipts deliveryReceipts { get; set; }
        public List<object> metadata { get; set; }
        public bool isStarred { get; set; }
        public int messageType { get; set; }
        public object attachments { get; set; }
        public ReturnReceipts returnReceipts { get; set; }
    }

    public class ReturnReceipts
    {
        public int status { get; set; }
    }

    public class User
    {
        public string userId { get; set; }
        public string displayName { get; set; }
        public string avatarInitials { get; set; }
    }
}
