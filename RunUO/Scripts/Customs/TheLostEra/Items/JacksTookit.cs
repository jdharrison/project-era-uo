namespace Server.Items
{
	public sealed class JacksToolkit : TinkerTools
	{
		[Constructable]
		public JacksToolkit() : base( int.MaxValue )
		{
			LootType = LootType.Blessed;
			Hue = 1895;
		}

		public override bool BreakOnDepletion { get { return false; } }
		public override bool ShowUsesRemaining { get { return false; } set {} }

		public override string DefaultName
		{
			get { return "Jack's Toolkit"; }
		}
	}
}
