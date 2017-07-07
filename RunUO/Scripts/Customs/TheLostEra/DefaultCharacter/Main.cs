using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Accounting;
using Server.Mobiles;
using Server.Misc;
using Server.Network;
using Server.Items;

namespace TheLostEra.DefaultCharacter
{
    public class Main
    {
        [CallPriority(Int32.MaxValue)]
        public static void Initialize()
        {
            EventSink.AccountLogin += new AccountLoginEventHandler(EventSink_AccountLogin);
            Console.ForegroundColor = System.ConsoleColor.DarkMagenta;
            Console.WriteLine("Default Character Loaded.");
            Console.ForegroundColor = System.ConsoleColor.White;
        }

        private static Mobile CreateMobile(Account a)
        {
            if (a.Count >= a.Limit)
                return null;

            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] == null)
                    return (a[i] = new PlayerMobile());
            }

            return null;
        }

        private static void EventSink_CharacterCreated(CharacterCreatedEventArgs args)
        {
            args.Profession = 0;

	        NetState state = null;
            if (args.State != null)
	            state = args.State;

            Mobile newChar = CreateMobile(args.Account as Account);

            if (newChar == null)
            {
                Utility.PushColor(ConsoleColor.Red);
                Console.WriteLine("Login: {0}: Character creation failed, account full", state == null ? "" : state.Address.ToString());
                Utility.PopColor();
                return;
            }

	        newChar.Player = true;
	        newChar.AccessLevel = args.Account.AccessLevel;

	        newChar.Race = Race.DefaultRace;
	        newChar.Female = Utility.RandomBool();
	        newChar.Hue = newChar.Race.RandomSkinHue();
	        ((PlayerMobile)newChar).SandMining = true;
	        ((PlayerMobile)newChar).Glassblowing = true;
	        newChar.Hunger = 20;
	        newChar.Name = NameList.RandomName(newChar.Female ? "female" : "male");

	        Utility.AssignRandomHair(newChar,true);
	        if (!newChar.Female)
		        Utility.AssignRandomFacialHair(newChar, true);

	        AddBackpack(newChar, new Item[]
	        {
				new JacksToolkit()
	        });

	        newChar.InitStats(25, 25, 25);

	        CityInfo city = new CityInfo("Lost Lands", "Encampment", 5208, 3609, 15, Map.Felucca);
	        newChar.MoveToWorld( city.Location, city.Map );

	        Console.WriteLine( "New character being created (account={0})", args.Account.Username );
	        Console.WriteLine( " - Character: {0} (serial={1})", newChar.Name, newChar.Serial );
	        Console.WriteLine( " - Started: {0} {1} in {2}", city.City, city.Location, city.Map );

	        new WelcomeTimer( newChar ).Start();
        }

        private static void AddBackpack(Mobile m, Item[] startingItems)
        {
            Container pack = m.Backpack;

            if (pack == null)
            {
                pack = new Backpack();
                pack.Movable = false;
                m.AddItem(pack);
            }

	        if(startingItems != null)
		        foreach(Item item in startingItems)
			        pack.AddItem(item);
        }


        public static void EventSink_AccountLogin(AccountLoginEventArgs e)
        {
            if (e.Accepted)
            {
                IAccount acc = Accounts.GetAccount(e.Username);
                if (acc == null || acc.Count > 0)
                    return;
                CharacterCreatedEventArgs arg = new CharacterCreatedEventArgs(
                    null,
                    acc,
                    "Create New Character",
                    false,
                    33770,
                    100,
                    100,
                    100,
                    new CityInfo("", "", 1, 1, 1),
                    null,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    Race.Human
                    );
                EventSink_CharacterCreated(arg);
                Console.ForegroundColor = System.ConsoleColor.DarkMagenta;
                Console.WriteLine("New character created for account " + acc.Username);
                Console.ForegroundColor = System.ConsoleColor.White;
            }
        }
    }
}
