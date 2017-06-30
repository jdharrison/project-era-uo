using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Bittiez.BoxerChat;
using Bittiez;
namespace Server.Gumps
{
    public class MinChatGump : Gump
    {
        public CHAN c;
        public MinChatGump(Mobile from, CHAN p)
            : base(SETTINGS.START_X, SETTINGS.START_Y)
        {
            c = p;
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            AddBackground(0, 0, 118, 36, 2620);
            AddPage(0);
            AddButton(5, 5, 2445, 2445, 1001, GumpButtonType.Reply, 1001);
            AddLabel(5 + 22, 5, 42, "Show Chat");
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile p = sender.Mobile;

            switch (info.ButtonID)
            {
                case 0:
                    {

                        break;
                    }
                case 1001:
                    {
                        if (p.HasGump(typeof(ChatGump)))
                            p.CloseGump(typeof(ChatGump));
                        p.SendGump(new ChatGump(p, (int)c));
                        break;
                    }

            }
        }
	}
}
