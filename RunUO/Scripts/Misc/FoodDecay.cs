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

		public FoodDecayTimer() : base( TimeSpan.FromMinutes( 5 ), TimeSpan.FromMinutes( 5 ) )
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

			m.Hunger -= 1;

			if(m.Hunger < 0)
			{
				m.SendMessage(0x21, "You are dying of hunger!");
				m.Damage((int)Math.Abs(Math.Pow(m.Hunger, 2)));
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
