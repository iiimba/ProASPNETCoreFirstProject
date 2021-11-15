using System;

namespace GOFPatternExamples.Decorator
{
    abstract class SenderDecorator : Sender
    {
        protected Sender _sender;

        public SenderDecorator(Sender sender)
        {
            _sender = sender;
        }

        public override void Send(string address, string message)
        {
            _sender.Send(address, message);
        }
    }

    class FacebookSender : SenderDecorator
    {
        public FacebookSender(Sender sender) : base(sender)
        {

        }

        public override void Send(string address, string message)
        {
            base.Send(address, message);
            SendToFacebook();
        }

        /// <summary>
        /// Added behaviour
        /// </summary>
        public void SendToFacebook()
        {
            Console.WriteLine("Send to facebook");
        }
    }

    class SkypeSender : SenderDecorator
    {
        /// <summary>
        /// Added state
        /// </summary>
        public string NickName { get; set; }

        public SkypeSender(Sender sender) : base(sender)
        {

        }

        public override void Send(string address, string message)
        {
            base.Send(address, message);
            SendToSkype();
        }

        /// <summary>
        /// Added behaviour
        /// </summary>
        public void SendToSkype()
        {
            if (!string.IsNullOrWhiteSpace(NickName))
            {
                Console.WriteLine($"Send to skype, nickname: {NickName}");
            }
        }
    }
}
