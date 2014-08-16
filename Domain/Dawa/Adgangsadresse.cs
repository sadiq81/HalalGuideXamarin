using System;
using System.Collections.Generic;

namespace HalalGuide.Domain.Dawa
{
	public class Adgangsadresse
	{
		public string Href { get; set; }

		public string Id { get; set; }

		public VejStykke Vejstykke { get; set; }

		public string Husnr { get; set; }

		public string Supplerendebynavn { get; set; }

		public Postnummer Postnummer { get; set; }

		public Kommune Kommune { get; set; }

		public Ejerlav Ejerlav { get; set; }

		public string Matrikelnr { get; set; }

		public string Esrejendomsnr { get; set; }

		public Historik Historik{ get; set; }

		public Adgangspunkt Adgangspunkt { get; set; }

		public DDKN DDKN { get; set; }

		public Sogn Sogn { get; set; }

		public Region Region { get; set; }

		public Retskreds Retskreds { get; set; }

		public Politikreds Politikreds { get; set; }

		public Opstillingskreds Opstillingskreds { get; set; }

		public Adgangsadresse ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[Adgangsadresse: Id={0}, Vejstykke={1}, Husnr={2}, Adgangspunkt={3}]", Id, Vejstykke, Husnr, Adgangspunkt);
		}
		
	}

	public class VejStykke
	{

		public string Href { get; set; }

		public string Kode { get; set; }

		public string Navn { get; set; }

		public override string ToString ()
		{
			return string.Format ("[VejStykke: Navn={0}]", Navn);
		}
		
	}


	public class Postnummer
	{
	
		public string Href { get; set; }

		public string Nr { get; set; }

		public string Navn { get; set; }

		public StormodtagerAdresser[] Stormodtageradresser { get; set; }

		public List<Kommuner> Kommuner { get; set; }

		public override string ToString ()
		{
			return string.Format ("[Postnummer: Nr={0}, Navn={1}]", Nr, Navn);
		}
		
	}

	public class StormodtagerAdresser
	{

		public string Href { get; set; }

		public string Nr { get; set; }
	}

	public class Kommuner
	{
		public string Href { get; set; }

		public string Kode { get; set; }

		public string Navn { get; set; }

		public override string ToString ()
		{
			return string.Format ("[Kommuner: Navn={0}]", Navn);
		}
		
	}

	public class Kommune
	{

		public string Href { get; set; }

		public string Kode { get; set; }

		public string Navn { get; set; }

		public override string ToString ()
		{
			return string.Format ("[Kommune: Navn={0}]", Navn);
		}
		
	}

	public class Ejerlav
	{
		public string Kode { get; set; }

		public string Navn { get; set; }
	}

	public class Historik
	{
		public string Oprettet { get; set; }

		public string Ændret { get; set; }
	}

	public class Adgangspunkt
	{

		public List<string> Koordinater { get; set; }

		public decimal Item { get; set; }

		public string Nøjagtighed { get; set; }

		public int Kilde { get; set; }

		public string Tekniskstandard { get; set; }

		public string Tekstretning { get; set; }

		public string Ændret { get; set; }

		public override string ToString ()
		{
			return string.Format ("[Adgangspunkt: Koordinater={0}]", Koordinater);
		}
		
	}

	public class DDKN
	{

		public string M100 { get; set; }

		public string Km1 { get; set; }

		public string Km10 { get; set; }

	}

	public class Sogn
	{
		public string Kode { get; set; }

		public string Navn { get; set; }
	}

	public class Region
	{
		public string Kode { get; set; }

		public string Navn { get; set; }
	}

	public class Retskreds
	{
		public string Kode { get; set; }

		public string Navn { get; set; }
	}

	public class Politikreds
	{
		public string Kode { get; set; }

		public string Navn { get; set; }
	}

	public class Opstillingskreds
	{
		public string Kode { get; set; }

		public string Navn { get; set; }
	}


}

