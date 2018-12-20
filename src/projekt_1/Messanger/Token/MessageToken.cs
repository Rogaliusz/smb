using System;
namespace projekt_1.Messanger.Token
{
    public class MessageToken : IMessageToken
    {
        public Guid Id { get; set; }

        public MessageToken()
        {
            Id = Guid.NewGuid();
        }
    }
}
