using System;
using System.Collections;
using System.Collections.Generic;
using projekt_1.Messanger.Messages;
using projekt_1.Messanger.Subscriptions;
using projekt_1.Messanger.Token;

namespace projekt_1.Messanger
{
    public class Messanger : IMessanger
    {
        private readonly IDictionary<Type, IList<SubscriptionBase>> _subscribers = new Dictionary<Type, IList<SubscriptionBase>>();

        public Messanger()
        {

        }

        public void Publish(MessageBase message)
        {
            var type = message.GetType();

            if (!_subscribers.ContainsKey(type))
            {
                return;
            }

            foreach (var subscription in _subscribers[type])
            {
                subscription.Invoke(message);
            }
        }

        public IMessageToken Subscribe<TMessage>(Action<TMessage> method) where TMessage : MessageBase
        {
            var type = typeof(TMessage);
            var token = new MessageToken();

            if (!_subscribers.ContainsKey(type))
            {
                _subscribers.Add(type, new List<SubscriptionBase>());
            }

            _subscribers[type].Add(new Subscription<TMessage>(method, token));

            return token;
        }
    }
}
