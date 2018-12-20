using System;
using projekt_1.Messanger.Messages;
using projekt_1.Messanger.Token;

namespace projekt_1.Messanger.Subscriptions
{
    public abstract class SubscriptionBase
    {
        public IMessageToken Token { get; private set; }

        protected SubscriptionBase(IMessageToken token)
        {
            Token = token;
        }

        public abstract void Invoke(MessageBase message);
    }
}
