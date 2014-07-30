using System;
using System.Xml.Serialization;

namespace HalalGuide.Services.RestDomain
{
	[XmlRoot ("postnummer")]
	public class Postnummer
	{
		[XmlElement ("nr")]
		public string Nr { get; set; }

		[XmlElement ("fra")]
		public string Fra { get; set; }

		[XmlElement ("til")]
		public string Til { get; set; }

		[XmlElement ("navn")]
		public string Navn { get; set; }

		public Postnummer ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[Postnummer: Nr={0}, Fra={1}, Til={2}, Navn={3}]", Nr, Fra, Til, Navn);
		}
		
	}
}

