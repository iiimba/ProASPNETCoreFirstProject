using System;

namespace GOFPatternExamples.Decorator
{
    class EmailSender : Sender
    {
        public override void Send(string address, string message)
        {
            Console.WriteLine($"Send to email: {address}, message: {message}");
        }
    }
}
