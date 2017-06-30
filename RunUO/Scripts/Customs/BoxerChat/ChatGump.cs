using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Bittiez.BoxerChat;
using Bittiez;

namespace Server.Gumps
{
    public class ChatGump : Gump
    {
        Mobile caller;
        public string CurData = "";
        private int w, h, _PAGE = 1;
        private CHAN CHANPAGE = CHAN.WORLD;
        private int Cur_Hue = 38, Other_Hue = 42;
        private int Button_Dif = 26;
        public int PAGE { get { return _PAGE; } set { _PAGE = value; } }

        public ChatGump(Mobile from, int Page)
            : base(SETTINGS.START_X, SETTINGS.START_Y)
        {
            #region Set Pages And Text
            if (Page == (int)CHAN.WORLD)
            {
                PAGE = (int)CHAN.WORLD;
                CHANPAGE = CHAN.WORLD;
                CurData = Init.Chat_Server.GetChatMessages(Init.Chat_Server.Messages);
            }
            else if (Page == (int)CHAN.GUILD)
            {
                PAGE = (int)CHAN.GUILD;
                CHANPAGE = CHAN.GUILD;
                CurData = Init.Chat_Server.GetGuildMessages(Init.Chat_Server.Guild_Messages, from);
            }
            else if (Page == (int)CHAN.STAFF)
            {
                PAGE = (int)CHAN.STAFF;
                CHANPAGE = CHAN.STAFF;
                CurData = Init.Chat_Server.GetChatMessages(Init.Chat_Server.Staff_Messages);
            }
            #endregion
            #region Required Stuff
            caller = from;
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = true;
            #endregion
            #region Background
            AddPage(0);
            w = 496; h = 160;

            AddBackground(0, 0, w, h, 2620);
            #endregion
            AddPage(1);

            Add_Text(CurData);

            Set_Buttons(Page, from);

            SendBoxGump(from);
        }

        public static void SendBoxGump(Mobile Chatter)
        {
            if (SETTINGS.Chat_Input_Gump_Enabled)
                if (!Chatter.HasGump(typeof(ChatBox)))
                    Chatter.SendGump(new ChatBox(Chatter, 1, SETTINGS.START_X, 160 + SETTINGS.START_Y));
        }


        public void Add_Text(string text)
        {
            AddHtml(5, 5, w - 10, h - 30, CurData, true, true);
        }

        public void Set_Buttons(int page, Mobile who)
        {
            int o = 5;
            AddButton(w, 0, 2710, 2711, 1004, GumpButtonType.Reply, 1004);
            if (SETTINGS.Enable_World_Tab)
            {
                AddButton(o, h - Button_Dif, 2445, 2445, 1001, GumpButtonType.Reply, 1001);//World
                AddLabel(o + SETTINGS.WORLD_OFFSET, h - Button_Dif, page == (int)CHAN.WORLD ? Cur_Hue : Other_Hue, SETTINGS.WORLD_TAB);
            }

            if (SETTINGS.Enable_Guild_Tab && Init.Show_For_Guild(who))
            {
                o = 113;
                AddButton(o, h - Button_Dif, 2445, 2445, 1002, GumpButtonType.Reply, 1002);//Guild
                AddLabel(o + SETTINGS.GUILD_OFFSET, h - Button_Dif, page == (int)CHAN.GUILD ? Cur_Hue : Other_Hue, SETTINGS.GUILD_TAB);
            }
            if (SETTINGS.Enable_Staff_Tab)
            {
                if (who.AccessLevel >= SETTINGS.Staff_Tab_Access_Level)
                {
                    o = w - (108 + 5);
                    AddButton(o, h - Button_Dif, 2445, 2445, 1003, GumpButtonType.Reply, 1003);//Staff
                    AddLabel(o + SETTINGS.STAFF_OFFSET, h - Button_Dif, page == (int)CHAN.STAFF ? Cur_Hue : Other_Hue, SETTINGS.STAFF_TAB);
                }
            }
        }



        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile p = sender.Mobile;

            switch (info.ButtonID)
            {
                case 0:
                    {
                        if (!p.HasGump(typeof(ChatBox)))
                            p.CloseGump(typeof(ChatBox));
                        break;
                    }
                case 1001:
                    {
                        if (p.HasGump(typeof(ChatGump)))
                            p.CloseGump(typeof(ChatGump));
                        p.SendGump(new ChatGump(p, (int)CHAN.WORLD));
                        break;
                    }
                case 1002:
                    {
                        if (p.HasGump(typeof(ChatGump)))
                            p.CloseGump(typeof(ChatGump));
                        p.SendGump(new ChatGump(p, (int)CHAN.GUILD));

                        break;
                    }
                case 1003:
                    {
                        if (p.HasGump(typeof(ChatGump)))
                            p.CloseGump(typeof(ChatGump));
                        p.SendGump(new ChatGump(p, (int)CHAN.STAFF));
                        break;
                    }
                case 1004:
                    {
                        if (p.HasGump(typeof(ChatGump)))
                            p.CloseGump(typeof(ChatGump));
                        if (p.HasGump(typeof(ChatBox)))
                            p.CloseGump(typeof(ChatBox));
                        p.SendGump(new MinChatGump(p, CHANPAGE));
                        break;
                    }

            }
        }
    }
}