using PublishR.Messaging;

namespace PublishR.Sample.MessageLibrary
{
    public class OrderCreatedMessage : PublishrMessage
    {
        public string Message { get; set; }
        public int OrderId { get; set; }
    }
}