using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class Araba
	{
		[Key]
		public int id { get; set; }

		public string arabadi { get; set; }

		public string arabakm { get; set; }

		public string arabamotor { get; set; }

		public string arabavitestipi { get; set; }

		public string arabayakıt { get; set; }

		public string arabaacıklama { get; set; }

		public string fiyat { get; set; }

		public string Resim { get; set; }




		[ForeignKey(nameof(markaid))]
		public int markaid { get; set; }


		public Marka Marka { get; set; }
	}
}
