using System;
using System.Collections.Generic;
using System.Text;
using Server.Commands;
using Server;
using Server.Gumps;
using Bittiez.BoxerChat;
using Bittiez;

namespace Bittiez.BoxerChat
{
    public class Chat
    {
        public static void Initialize()
        {
            if (SETTINGS.BoxerChat_Enabled)
            {
                if (SETTINGS.Enable_World_Tab)
                    foreach (string pre in SETTINGS.COMMAND_PREFIX)
                        CommandSystem.Register(pre, AccessLevel.Player, new CommandEventHandler(On_Chat_World));
                if (SETTINGS.Enable_Guild_Tab)
                    foreach (string pre in SETTINGS.COMMAND_PREFIX_GUILD)
                        CommandSystem.Register(pre, AccessLevel.Player, new CommandEventHandler(On_Chat_Guild));
                if (SETTINGS.Enable_Staff_Tab)
                    foreach (string pre in SETTINGS.COMMAND_PREFIX_STAFF)
                        CommandSystem.Register(pre, AccessLevel.GameMaster, new CommandEventHandler(On_Chat_Staff));
                foreach (string pre in SETTINGS.ONLINE_LIST_PREFIX)
                    CommandSystem.Register(pre, AccessLevel.Player, new CommandEventHandler(Online_List));
                if (SETTINGS.Open_On_Login)
                {
                    EventSink.Login += new LoginEventHandler(EventSink_Login);
                    EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_PlayerDeath);
                }
            }
        }
        public static void EventSink_PlayerDeath(PlayerDeathEventArgs e)
        {
            SendGump((int)SETTINGS.Default_Tab, e.Mobile);
            ChatGump.SendBoxGump(e.Mobile);
        }

        public static void EventSink_Login(LoginEventArgs e)
        {
            SendGump((int)SETTINGS.Default_Tab, e.Mobile);
            ChatGump.SendBoxGump(e.Mobile);
        }

        public static void Online_List(CommandEventArgs e)
        {
            e.Mobile.SendGump(new OnlineGump(e.Mobile));
        }

        public static void On_Chat_Staff(CommandEventArgs e)
        {
            if (e.ArgString == null || e.ArgString == "")
            {
                if (SETTINGS.ONLINE_LIST_NO_TEXT)
                    e.Mobile.SendGump(new OnlineGump(e.Mobile));
                return;
            }
            Mobile Chatter = e.Mobile;
            string Message = e.ArgString.Trim();
            Init CS = Init.Chat_Server;
            CS.LastMessage = CHAN.STAFF;
            AddMessage(CS.Staff_Messages, Chatter, Message, 3);
            SendGump((int)CHAN.STAFF, Chatter); //Send the chatter the gump if it is closed
        }

        public static void On_Chat_World(CommandEventArgs e)
        {
            if (e.ArgString == null || e.ArgString == "")
            {
                if (SETTINGS.ONLINE_LIST_NO_TEXT)
                    e.Mobile.SendGump(new OnlineGump(e.Mobile));
                return;
            }
            Mobile Chatter = e.Mobile;
            string Message = e.ArgString.Trim();
            Init CS = Init.Chat_Server;
            CS.LastMessage = CHAN.WORLD;
            AddMessage(CS.Messages, Chatter, Message, 1);
            SendGump((int)CHAN.WORLD, Chatter); //Send the chatter the gump if it is closed
        }

        public static void On_Chat_Guild(CommandEventArgs e)
        {
            if (e.ArgString == null || e.ArgString == "")
            {
                if (SETTINGS.ONLINE_LIST_NO_TEXT)
                    e.Mobile.SendGump(new OnlineGump(e.Mobile));
                return;
            }
            Mobile Chatter = e.Mobile;
            if (Init.Show_For_Guild(Chatter))
            {
                string Message = e.ArgString.Trim();
                Init CS = Init.Chat_Server;
                CS.LastMessage = CHAN.GUILD;
                AddMessage(CS.Guild_Messages, Chatter, Message, 2);
                SendGump((int)CHAN.GUILD, Chatter); //Send the chatter the gump if it is closed
            }
        }

        public static void AddMessage(List<Message> MS, Mobile Chatter, String Message, int Page)
        {
            Init CS = Init.Chat_Server;

            foreach (string f in SETTINGS.FILTER)
                Message = Message.Replace(f, SETTINGS.FILTER_WITH);

            if (Chatter == null || CS == null || Message.Length < SETTINGS.Min_Message_Length)
                return;

            if (Page == (int)CHAN.GUILD)
                CS.Add_Guild_Message(Chatter, Message, MS);
            else
                CS.Add_Message(Chatter, Message, MS);
            UpdateGump(Page);
        }

        public static void SendGump(int Page, Mobile Chatter)
        {
            if (!Chatter.HasGump(typeof(ChatGump)))
                Chatter.SendGump(new ChatGump(Chatter, Page));
        }

        public static void UpdateGump(int Page)
        {
            #region Declares
            ChatGump CG;
            int CP;
            Init CS = Init.Chat_Server;
            List<Mobile> Online_Players = Bittiez.Tools.List_Connected_Players();
            #endregion
            foreach (Mobile p in Online_Players)
            {
                if (p != null && CS != null)
                {
                    if (p.HasGump(typeof(ChatGump)))
                    {
                        CG = (ChatGump)p.FindGump(typeof(ChatGump));
                        CP = CG.PAGE;
                        if (CP == Page)
                        {
                            p.CloseGump(typeof(ChatGump));
                            p.SendGump(new ChatGump(p, CP));
                        }
                    }
                    else
                    {

                        if (SETTINGS.Send_Message_As_SysMessage_If_Gump_Closed)
                        {

                            if (CS.LastMessage == CHAN.WORLD && CS.Messages.Count > 0)
                                p.SendMessage((CS.Messages[CS.Messages.Count - 1].MOBILE.RawName + ": " + CS.Messages[CS.Messages.Count - 1].MESSAGE));
                            if ((CS.LastMessage == CHAN.STAFF) && (CS.Staff_Messages.Count > 0) && (p.AccessLevel == SETTINGS.Staff_Tab_Access_Level || p.AccessLevel > SETTINGS.Staff_Tab_Access_Level))
                                p.SendMessage((CS.Staff_Messages[CS.Staff_Messages.Count - 1].MOBILE.RawName + ": " + CS.Staff_Messages[CS.Staff_Messages.Count - 1].MESSAGE));
                            if ((CS.LastMessage == CHAN.GUILD) && (CS.Guild_Messages.Count > 0))
                            {
                                if (p.Guild == CS.Guild_Messages[CS.Guild_Messages.Count - 1].MOBILE.Guild)
                                {
                                    p.SendMessage((CS.Guild_Messages[CS.Guild_Messages.Count - 1].MOBILE.RawName + ": " + CS.Guild_Messages[CS.Guild_Messages.Count - 1].MESSAGE));
                                }
                            }

                        }
                    }
                }
            }
        }

    }
}
