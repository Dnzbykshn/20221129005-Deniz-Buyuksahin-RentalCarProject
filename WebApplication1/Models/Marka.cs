using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class Marka
	{
		[Key]
		public int id { get; set; }
		public string ArabaMarka { get; set; }

		public ICollection<Araba> Araba { get; set; }
	}
}
