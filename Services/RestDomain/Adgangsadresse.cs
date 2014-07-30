using System;
using System.Xml.Serialization;
using System.Globalization;

namespace HalalGuide.Services.RestDomain
{
	[XmlRoot ("adgangsadresse")]
	public class Adgangsadresse
	{
		[XmlElement ("id")]
		public string Id { get; set; }

		[XmlElement ("vejnavn")]
		public Vejnavn VejNavn { get; set; }

		[XmlElement ("postnummer")]
		public Postnummer Postnummer { get; set; }

		[XmlElement ("husnr")]
		public string Husnr { get; set; }

		public Adgangsadresse ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[Adgangsadresse: Id={0}, VejNavn={1}, Postnummer={2}, Husnr={3}]", Id, VejNavn, Postnummer, Husnr);
		}
		
	}
}

