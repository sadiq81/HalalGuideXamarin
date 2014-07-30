using System.Xml.Serialization;

namespace HalalGuide.Services.RestDomain
{
	[XmlRoot ("wgs84koordinat")]
	public class WGS84Koordinat
	{
		[XmlElement ("bredde")]
		public string Bredde { get; set; }

		[XmlElement ("længde")]
		public string Længde { get; set; }

		public WGS84Koordinat ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[WGS84Koordinat: Bredde={0}, Længde={1}]", Bredde, Længde);
		}
		
	}
}

