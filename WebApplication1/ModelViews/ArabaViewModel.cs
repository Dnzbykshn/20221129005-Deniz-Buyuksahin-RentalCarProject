using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ModelViews
{
	public class ArabaViewModel
	{
		[Key]
		public int id { get; set; }


		[Display(Name = "Araba Adı Giriniz: ")]
		[Required(ErrorMessage = "Araba Adı Giriniz: !")]
		public string arabadi { get; set; }




		[Display(Name = "Araba KM Giriniz: ")]
		[Required(ErrorMessage = "Araba KM  giriniz: !")]
		public string arabakm { get; set; }

		[Display(Name = "Araba Tipi Giriniz: (Sedan/HatchBack/Suv) ")]
		[Required(ErrorMessage = "Araba Motor  giriniz: !")]
		public string arabamotor { get; set; }


		[Display(Name = "Araba Vites Tipi Giriniz: ")]
		[Required(ErrorMessage = "Araba Vites Tipi  giriniz: !")]
		public string arabavitestipi { get; set; }



		[Display(Name = "Araba Yakıt Tipi Giriniz: ")]
		[Required(ErrorMessage = "Araba Yakıt Tipi  giriniz: !")]
		public string arabayakıt { get; set; }


		[Display(Name = "Araba Açıklama Giriniz: ")]
		[Required(ErrorMessage = "Araba Açıklama  giriniz: !")]
		public string arabaacıklama { get; set; }


		[Display(Name = "Araba Fiyat Giriniz: ")]
		[Required(ErrorMessage = "Araba Fiyat  giriniz: !")]
		public string fiyat { get; set; }

		[NotMapped]
		public IFormFile Resim { get; set; }


		public string ResimData { get; set; }


		[Display(Name = "Marka Tipi Seçiniz: ")]
		public int markaid { get; set; }
	}
}
