using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Bittiez.BoxerChat;
using Bittiez;

namespace Bittiez.BoxerChat
{
    public class SETTINGS
    {
        public static bool BoxerChat_Enabled = true;

        #region MISC
        // __  __ _____  _____  _____ 
        //|  \/  |_   _|/ ____|/ ____|
        //| \  / | | | | (___ | |     
        //| |\/| | | |  \___ \| |     
        //| |  | |_| |_ ____) | |____ 
        //|_|  |_|_____|_____/ \_____|                             

        public static int START_X = 0;//Start left/right position
        public static int START_Y = 600;//Start top/bottom position
        public static int WORLD_OFFSET = 40;//Text offset from the button behind it        
        public static int GUILD_OFFSET = 40;//Text offset from the button behind it        
        public static int STAFF_OFFSET = 40;//Text offset from the button behind it
        #endregion
        #region TEXT
        // _______ ________   _________ 
        //|__   __|  ____\ \ / /__   __|
        //   | |  | |__   \ V /   | |   
        //   | |  |  __|   > <    | |   
        //   | |  | |____ / . \   | |   
        //   |_|  |______/_/ \_\  |_|                                 

        public static string NO_MESSAGES = @"No messages.";//Message to display when there are no messages
        public static string Chat_Fail = "You must wait a few seconds to chat again."; //Message to send when they need to wait longer to send a message OR if their message length is not long enough
        public static string WORLD_TAB = "World"; //World tab name
        public static string GUILD_TAB = "Guild"; //Guild tab name
        public static string STAFF_TAB = "Staff"; //Staff tab name
        #endregion
        #region ON \ OFF
        //  ____  _   _  __        ____  ______ ______ 
        // / __ \| \ | | \ \      / __ \|  ____|  ____|
        //| |  | |  \| |  \ \    | |  | | |__  | |__   
        //| |  | | . ` |   \ \   | |  | |  __| |  __|  
        //| |__| | |\  |    \ \  | |__| | |    | |     
        // \____/|_| \_|     \_\  \____/|_|    |_|     

        public static bool Chat_Input_Gump_Enabled = true; //Enable or disable the chat input gump(Not the chat gump itself, the mini chat box under it)
        public static bool REVERSE_MESSAGES = true;//Display newest messages at the top of the box if true
        public static bool Color_Names = true; //Color names based on the colors set below?
        public static bool Enable_World_Tab = true; //Enable the world tab?
        public static bool Enable_Guild_Tab = true; //Enable the guild tab?
        public static bool Enable_Staff_Tab = true; //Enable the staff tab?
        public static bool Open_On_Login = true; //Open the chat dialog on login?
        public static bool Disable_Guild_When_Not_In_Guild = true; //Disable the guild for players not in a guild
        #endregion
        #region FUNCTIONALITY
        // _______ _    _ ______  __          ______  _____  _  __ _____ 
        //|__   __| |  | |  ____| \ \        / / __ \|  __ \| |/ // ____|
        //   | |  | |__| | |__     \ \  /\  / / |  | | |__) | ' /| (___  
        //   | |  |  __  |  __|     \ \/  \/ /| |  | |  _  /|  <  \___ \ 
        //   | |  | |  | | |____     \  /\  / | |__| | | \ \| . \ ____) |
        //   |_|  |_|  |_|______|     \/  \/   \____/|_|  \_\_|\_\_____/ 

        public static string[] COMMAND_PREFIX = { "c", "t", "message", "chat", "talk", "msg" }; //[Command prefixes to use for World Chat
        public static string[] COMMAND_PREFIX_GUILD = { "g", "guild" }; //[Command prefixes to use for Guild Chat
        public static string[] COMMAND_PREFIX_STAFF = { "staff" }; //[Command prefixes to use for Staff Chat(Note: Apparantly [s is built into RunUO or ServUO already, so you will need to use a different [pre)
        public static string[] ONLINE_LIST_PREFIX = { "online", "o" }; //[Command prefixes to use for the online list
        public static bool ONLINE_LIST_NO_TEXT = true; //Turn to to false if you don't want all command prefixes to open the online menu(When a player types [c without any text it will open the online list if this is set to true)
        public static string[] FILTER = { "fuck", "shit", "bitch", "ass", "cunt" }; //Any occurances of these words will be replace by the word below
        public static string FILTER_WITH = "****"; //The above words will be replaced with this                                                                
        public static int Max_Messages = 50; //The maximum amount of messages to keep in the chat gump
        public static TimeSpan Chat_Delay = TimeSpan.FromSeconds(5); //The delay between sending messages
        public static int Min_Message_Length = 1; //Minimum message length
        public static AccessLevel Staff_Tab_Access_Level = AccessLevel.Counselor; //What access level should we show the staff tab at?
        public static AccessLevel Overwrite_Chat_Delay_Level = AccessLevel.Counselor; //At what accesslevel should we ignore the chat delay
        public static int Guild_Max_Messages = 100; //All guild messages are counted together, however only messages from the same guild are displayed for each user, so because this i recommend a higher max for guild messages
        public static CHAN Default_Tab = CHAN.WORLD; //Default tab to show
        public static bool Online_List_ShowStaff = true; //Show staff on the online list?
        public static bool Send_Message_As_SysMessage_If_Gump_Closed = true; //When someone writes a message(in the world tab), and a player has the gump closed, should we show the message as a System Message?(Mobile.SendMessage())
        #endregion
        #region COLORS
        //  _____ ____  _      ____  _____   _____ 
        // / ____/ __ \| |    / __ \|  __ \ / ____|
        //| |   | |  | | |   | |  | | |__) | (___  
        //| |   | |  | | |   | |  | |  _  / \___ \ 
        //| |___| |__| | |___| |__| | | \ \ ____) |
        // \_____\____/|______\____/|_|  \_\_____/ 

        //These are regular hue numbers
        public static int Online_List_Staff = 10;
        public static int Online_List_Players = 15;

        //These are hex colors, http://www.colorpicker.com/ will help you find colors.
        public static string[] NameColors =
        {   //TextColor
            "#000000",
            //Player 
            "#000000",
            //VIP 
            "#000000",
            //Counselor 
            "#1e25a1",
            //Decorator 
            "#1e25a1",
            //Spawner 
            "#1e25a1",
            //GameMaster 
            "#1ea127",
            //Seer 
            "#1ea127",
            //Administrator 
            "db151f",
            //Developer 
            "#db151f",
            //CoOwner
            "#db151f",
            //Owner 
            "#db151f"};
        #endregion
    }
}
