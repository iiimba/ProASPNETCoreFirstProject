using System;

namespace GOFPatternExamples.Bridge
{
    interface IMessageSender
    {
        void Send(string subject, string body);
    }

    class MailSender : IMessageSender
    {
        public void Send(string subject, string body)
        {
            Console.WriteLine($"Mail, {subject}, {body}");
        }
    }

    class WebRequestSender : IMessageSender
    {
        public void Send(string subject, string body)
        {
            Console.WriteLine($"WebRequest, {subject}, {body}");
        }
    }
}
