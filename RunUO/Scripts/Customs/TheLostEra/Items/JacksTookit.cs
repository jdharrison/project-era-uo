namespace Server.Items
{
	public class JacksToolkit : TinkerTools
	{
		[Constructable]
		public JacksToolkit() : base( int.MaxValue )
		{
			LootType = LootType.Blessed;
			Hue = 1895;
		}

		public JacksToolkit( Serial serial ) : base( serial )
		{
		}

		public override bool BreakOnDepletion { get { return false; } }
		public override bool ShowUsesRemaining { get { return false; } set {} }

		public override string DefaultName
		{
			get { return "Jack's Toolkit"; }
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
