using System;
namespace projekt_1.Messanger.Messages
{
    public abstract class MessageBase
    {
        public object Sender { get; private set; }

        public MessageBase(object sender)
        {
            Sender = sender;
        }
    }
}
