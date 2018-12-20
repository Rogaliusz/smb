
using projekt_1.Models;

namespace projekt_1.Messanger.Messages
{
    public class ShopCreatedMessage : MessageBase
    {
        public Shop Shop { get; private set; }

        public ShopCreatedMessage(Shop shop,
            object sender) : base(sender)
        {
            Shop = shop;
        }
    }
}
