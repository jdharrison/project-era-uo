using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Bittiez.BoxerChat;
using Bittiez;
namespace Server.Gumps
{
    public class ChatBox : Gump
    {
        Mobile caller;
        private int PAGE = 1, SX = 1, SY = 1;

        public ChatBox(Mobile from, int Page, int x, int y) : this(from, Page, x, y, "Type here") { }

        public ChatBox(Mobile from, int Page, int x, int y, string dt)
            : base(x, y)
        {
            #region Declares
            PAGE = Page;
            SX = x;
            SY = y;
            caller = from;
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            #endregion
            #region Background
            AddPage(0);
            AddBackground(0, 0, 447, 82, 2620);
            AddPage(1);
            #endregion
            AddTextEntry(5, 5, 437, 40, 38, 0, @dt);

            #region Buttons/Radios
            int o = 5, oo = 23, dif = 75;
            if (SETTINGS.Enable_World_Tab)
            {
                AddRadio(o, 55, 210, 211, Page == (int)CHAN.WORLD ? true : false, 1);
                AddLabel(o + oo, 54, 42, @SETTINGS.WORLD_TAB);
            }

            if (SETTINGS.Enable_Guild_Tab && Init.Show_For_Guild(from))
            {
                AddRadio(o + dif, 55, 210, 211, Page == (int)CHAN.GUILD ? true : false, 2);
                AddLabel(o + oo + dif, 54, 42, @SETTINGS.GUILD_TAB);
            }
            if (SETTINGS.Enable_Staff_Tab)
            {
                if (from.AccessLevel >= SETTINGS.Staff_Tab_Access_Level)
                {
                    AddRadio(o + dif + dif, 55, 210, 211, Page == (int)CHAN.STAFF ? true : false, 3);
                    AddLabel(o + oo + dif + dif, 54, 42, @SETTINGS.STAFF_TAB);
                }
            }
            #endregion
            int xx = 447 - 108, xxx = 82 - 26;
            AddButton(xx, xxx, 2445, 2445, 1001, GumpButtonType.Reply, 1001);//Send
            AddLabel(xx + 40, xxx, 42, "Send");
        }



        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case 0:
                    {

                        break;
                    }
                case 1001:
                    {
                        for (int i = 1; i < 4; i++)
                            if (info.IsSwitched(i))
                            {
                                string[] sep = { " " };
                                string text = info.TextEntries[0].Text;
                                string[] text1 = text.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                                CommandEventArgs e = new CommandEventArgs(from, "", text, text1);
                                switch (i)
                                {
                                    case (int)CHAN.WORLD: { Bittiez.BoxerChat.Chat.On_Chat_World(e); break; }
                                    case (int)CHAN.GUILD: { Bittiez.BoxerChat.Chat.On_Chat_Guild(e); break; }
                                    case (int)CHAN.STAFF: { Bittiez.BoxerChat.Chat.On_Chat_Staff(e); break; }
                                }
                                if (from.HasGump(typeof(ChatBox)))
                                    from.CloseGump(typeof(ChatBox));
                                from.SendGump(new ChatBox(from, i, SX, SY, ""));
                                break;
                            }
                        //Send text here..
                        break;
                    }

            }
        }
    }
}