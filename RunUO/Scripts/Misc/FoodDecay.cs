using System;
using Server.Network;
using Server;

namespace Server.Misc
{
	public class FoodDecayTimer : Timer
	{
		public static void Initialize()
		{
			new FoodDecayTimer().Start();
		}

		public FoodDecayTimer() : base( TimeSpan.FromMinutes( 1 ), TimeSpan.FromMinutes( 2 ) )
		{
			Priority = TimerPriority.OneMinute;
		}

		protected override void OnTick()
		{
			FoodDecay();
		}

		public static void FoodDecay()
		{
			foreach ( NetState state in NetState.Instances )
			{
				HungerDecay( state.Mobile );
				ThirstDecay( state.Mobile );
			}
		}

		public static void HungerDecay( Mobile m )
		{
			if (m == null)
				return;

			if (m.Hunger > 0)
				m.Hunger -= 1;

			if (m.Alive && m.Player)
			{
				if (m.Hunger == 0)
				{
					m.SendMessage(0x21, "You have died of hunger!");
					m.Kill();
					return;
				}

				if(m.Hunger < 5)
				{
					m.SendMessage(0x21, "You are dying of hunger! You need to eat soon.");
					m.Damage(5 * (5 - m.Hunger));
				}

				switch (m.Hunger)
				{
					case 5:
						m.SendMessage(0x21, "You are beginning to starve, and feel slow.");
						break;
					case 10:
						m.SendMessage("You feel hungry, but still feel normal.");
						break;
					case 15:
						m.SendMessage("You begin to feel hungry, but still feel sharp.");
						break;
				}
			}
		}

		public static void ThirstDecay( Mobile m )
		{
			if (m == null)
				return;

			if (m.Thirst >= 1)
				m.Thirst -= 1;
		}
	}
}
