namespace Server.Items
{
	public class WoodPulp : Item
	{
		public override string DefaultName { get { return "Wood Pulp"; } }

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public WoodPulp() : this(1)
		{
		}

		[Constructable]
		public WoodPulp(int amount) : base(0xF90)
		{
			Hue = 0x30;
			Stackable = true;
			Amount = amount;
		}

		public WoodPulp(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
