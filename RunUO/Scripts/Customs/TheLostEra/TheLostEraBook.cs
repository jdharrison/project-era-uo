using System;
using Server;

namespace Server.Items
{
	public class TheLostEraBook : RedBook
	{
               /*
Prelude

    The lands of this
world once were free
of the creatures that
serve the Shadow.

    It seemed like peace
would never end. Conflict,
poverty, and hunger were
nearly unheard of.

    That was until whispers
of a dark Shadow crept back
into the lands. One of an
ancient power that was long
forgotten through eras past.

   Our complacency was the
undoing of countless lives.
What started as towns and
folk slowly disappearing
quickly turned to panic.

    It was not long after
that it became rare to even
see a being that was not
in service with the Shadow.
There was only one that stood
among the desolated armies.

    His name was Jack. He was
not a man of honor, might or
of any nobility. He could barely
pass as a Farmer, for the
skill he possessed was leisure.
However, who could be judged of
such a thing in a time of peace.

   Soon all he knew and loved
was slaughtered, taken and
destroyed. In the moment that
he embraced death, a gift was
given to him. The gift to
die a thousand deaths but to
always return to life anew.

    From that he took strength
and rallied survivors. He then
ventured to the last known 
refuge from the Shadows.

               */

                public static readonly BookContent Content = new BookContent
                (
                        "The Lost Era", "Eldars",
                        new BookPageInfo
                        (
                            "Prelude",
                            "",
                            "The lands of this",
                            "world once were free",
                            "of the creature that",
                            "serve the Shadow."
                        )
                );

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public TheLostEraBook() : base( false )
		{
			Hue = 0x3E4;
		}

		public TheLostEraBook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
