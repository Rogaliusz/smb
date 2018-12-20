using System;
using projekt_1.Messanger.Messages;
using projekt_1.Messanger.Token;

namespace projekt_1.Messanger
{
    public interface IMessanger
    {
        IMessageToken Subscribe<TMessage>(Action<TMessage> method)
            where TMessage : MessageBase;

        void Publish(MessageBase message);
    }
}
