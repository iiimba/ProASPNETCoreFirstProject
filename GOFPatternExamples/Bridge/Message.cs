namespace GOFPatternExamples.Bridge
{
    abstract class Message
    {
        public Message(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        protected IMessageSender _messageSender;

        public string Subject { get; set; }

        public string Body { get; set; }

        public abstract void Send();
    }

    class SystemMessage : Message
    {
        public SystemMessage(IMessageSender messageSender)
            : base(messageSender)
        {

        }

        public override void Send()
        {
            _messageSender.Send(Subject, Body);
        }
    }

    class UserMessage : Message
    {
        public UserMessage(IMessageSender messageSender)
            : base(messageSender)
        {

        }

        public string Comment { get; set; }

        public override void Send()
        {
            var body = $"{Body}, {Comment}";
            _messageSender.Send(Subject, body);
        }
    }
}
