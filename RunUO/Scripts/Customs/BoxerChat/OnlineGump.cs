using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Commands;
using System.Collections.Generic;
using Server.Network;
using Server.Gumps;
namespace Server.Gumps
{
    public class OnlineGump : Gump
    {
        private Mobile caller;

        public OnlineGump(Mobile mobile)
            : base(50, 50)
        {
            bool ArrowPM = false;
            Type t = ScriptCompiler.FindTypeByFullName("Bittiez.ArrowPM");
            if (t != null)
            {
                ArrowPM = true;
            }
            caller = mobile;
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddBackground(0, 0, 300, 426, 3500);
            AddPage(1);


            int page = 0, cpage = 1;
            int max = 18;
            List<Mobile> name = Bittiez.Tools.List_Connected_Players();
            AddLabel(110, 5, 38, "Online Players");

            if (name != null)// && name.Count == count.Count)
                foreach (Mobile n in name)
                {
                    if (n == null)
                        continue;
                    if (!Bittiez.BoxerChat.SETTINGS.Online_List_ShowStaff && n.AccessLevel >= AccessLevel.Counselor)
                        continue;
                    if(n.AccessLevel >= AccessLevel.Counselor)
                        AddLabel(15, 23 + (20 * page), Bittiez.BoxerChat.SETTINGS.Online_List_Staff, string.Format("{0}", n.Name));
                    else
                        AddLabel(15, 23 + (20 * page), Bittiez.BoxerChat.SETTINGS.Online_List_Players, string.Format("{0}", n.Name));
                    //if(ArrowPM) 
                    page++;
                    if (page >= max)
                    {
                        page = 0;
                        cpage++;
                        AddButton(265, 390, 22405, 22405, 600, GumpButtonType.Page, cpage);

                        AddPage(cpage);
                        if (cpage > 1) AddButton(25, 390, 22402, 22402, 600, GumpButtonType.Page, cpage - 1);
                    }

                }

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

            }
        }
    }
}