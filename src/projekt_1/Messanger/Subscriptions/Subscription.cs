using System;
using projekt_1.Messanger.Messages;
using projekt_1.Messanger.Token;

namespace projekt_1.Messanger.Subscriptions
{
    public class Subscription<TMessage> : SubscriptionBase
        where TMessage : MessageBase
    {
        public Action<TMessage> Action { get; private set; }

        public Subscription(Action<TMessage> action, 
            IMessageToken token)
            : base(token)
        {
            Action = action;
        }

        public override void Invoke(MessageBase message)
        {
            Action.Invoke((TMessage)message);
        }
    }
}
