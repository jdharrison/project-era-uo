using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Bittiez.BoxerChat;
using Bittiez;

namespace Bittiez.BoxerChat
{
    public enum CHAN
    {
        WORLD = 1,
        GUILD = 2,
        STAFF = 3
    }

    public class Init
    {
        public static int Version = 15;
        public static Init Chat_Server = new Init();
        public List<Message> Messages = new List<Message>();
        public List<Message> Guild_Messages = new List<Message>();
        public List<Message> Staff_Messages = new List<Message>();
        private List<Chatter> Chatters = new List<Chatter>();
        public CHAN LastMessage = CHAN.WORLD;

        public static void Initialize()
        {
            Type t = ScriptCompiler.FindTypeByFullName("Bittiez.Tools");
            if (t == null)
            {
                Console.WriteLine("Bittiez Utilities is required for BoxerChat!");
                throw new Exception("Bittiez Utilities is required for BoxerChat!");
            }
            else
            {
                Bittiez.Tools.ConsoleWrite(ConsoleColor.Blue, "BoxerChat Version " + Tools.Format_Version(Version));
            }
        }

        public Init()
        {
        }

        public static bool Show_For_Guild(Mobile from)
        {
            bool Show = true;
            if (SETTINGS.Disable_Guild_When_Not_In_Guild)
                if (from.Guild == null)
                    Show = false;
            return Show;
        }

        public void Add_Message(Mobile from, string message, List<Message> MS)
        {
            if (from != null && message.Length >= SETTINGS.Min_Message_Length && AllowChat(from))
                MS.Add(new Message(from, @message, DateTime.Now));
            else
                from.SendMessage(SETTINGS.Chat_Fail);

            while (MS.Count > SETTINGS.Max_Messages)
                MS.RemoveAt(0);
        }

        public void Add_Guild_Message(Mobile from, string message, List<Message> MS)
        {
            if (from != null && message.Length >= SETTINGS.Min_Message_Length && AllowChat(from))
                MS.Add(new Message(from, @message, DateTime.Now));
            else
                from.SendMessage(SETTINGS.Chat_Fail);

            while (MS.Count > SETTINGS.Guild_Max_Messages)
                MS.RemoveAt(0);
        }

        public string GetChatMessages(List<Message> MS)
        {
            string ms = "", pre = "", suf = "";
            List<Message> MREV = new List<Message>();
            foreach (Message mm in MS)
                MREV.Add(mm);

            if(SETTINGS.REVERSE_MESSAGES) MREV.Reverse();
            foreach (Message m in MREV)
            {
                if (SETTINGS.Color_Names)
                {
                    pre = "<BASEFONT COLOR=" + SETTINGS.NameColors[((int)m.MOBILE.AccessLevel) + 1] + ">";
                    suf = "<BASEFONT COLOR=" + SETTINGS.NameColors[0] + ">";
                }


                ms += string.Format("[{0}] {1}: {2}<br>", m.DATETIME.ToShortTimeString(), pre + m.MOBILE.Name + suf, m.MESSAGE);
            }
            return ms.Length < 1 ? SETTINGS.NO_MESSAGES : ms;
        }

        public string GetGuildMessages(List<Message> MS, Mobile For)
        {
            string ms = "", pre = "", suf = "";
            List<Message> MREV = new List<Message>();
            foreach (Message mm in MS)
                MREV.Add(mm);

            if (SETTINGS.REVERSE_MESSAGES) MREV.Reverse();
            foreach (Message m in MREV)
            {
                if (m.MOBILE.Guild == For.Guild)
                {
                    if (SETTINGS.Color_Names)
                    {
                        pre = "<BASEFONT COLOR=" + SETTINGS.NameColors[((int)m.MOBILE.AccessLevel) + 1] + ">";
                        suf = "<BASEFONT COLOR=" + SETTINGS.NameColors[0] + ">";
                    }


                    ms += string.Format("[{0}] {1}: {2}<br>", m.DATETIME.ToShortTimeString(), pre + m.MOBILE.Name + suf, m.MESSAGE);
                }
            }
            return ms.Length < 1 ? SETTINGS.NO_MESSAGES : ms;
        }

        private bool AllowChat(Mobile from)
        {
            if (from.AccessLevel >= SETTINGS.Overwrite_Chat_Delay_Level)
                return true;
            foreach (Chatter ch in Chatters)
            {
                if (ch.MOBILE == from)
                {
                    DateTime m = DateTime.UtcNow;
                    if (m > ch.LASTCHAT.AddMilliseconds(SETTINGS.Chat_Delay.TotalMilliseconds))
                    {
                        ch.LASTCHAT = m;
                        return true;
                    }
                    else return false;
                }
            }

            Chatters.Add(new Chatter(from, DateTime.UtcNow));
            return true;
        }

    }

    public class Message
    {
        private Mobile m_Mobile;
        private string m_Message;
        private DateTime m_DateTime;

        public Mobile MOBILE { get { return m_Mobile; } set { m_Mobile = value; } }
        public string MESSAGE { get { return m_Message; } set { m_Message = value; } }
        public DateTime DATETIME { get { return m_DateTime; } set { m_DateTime = value; } }

        public Message(Mobile who, string message, DateTime datetime)
        {
            MOBILE = who;
            MESSAGE = message;
            DATETIME = datetime;
        }
    }

    public class Chatter
    {
        public Mobile MOBILE { get { return m; } set { m = value; } }
        private Mobile m;
        public DateTime LASTCHAT { get { return l; } set { l = value; } }
        private DateTime l;
        public Chatter(Mobile mobile, DateTime datetime)
        {
            m = mobile;
            l = datetime;
        }
    }
}
