using System.Xml.Serialization;

namespace HalalGuide.Services.RestDomain
{
	[XmlRoot ("vejnavn")]
	public class Vejnavn
	{
		[XmlElement ("kode")]
		public string Kode { get; set; }

		[XmlElement ("navn")]
		public string Navn { get; set; }

		public Vejnavn ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[Vejnavn: Kode={0}, Navn={1}]", Kode, Navn);
		}
		
	}
}

