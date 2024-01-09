using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Net;
using WebApplication1.Models;
using WebApplication1.ModelViews;

namespace WebApplication1.Controllers
{
	
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notify;
        private readonly AppDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AdminController(IConfiguration configuration , INotyfService notyfService , AppDbContext context, IFileProvider fileProvider , UserManager<AppUser> userManager , RoleManager<AppRole> roleManager = null, SignInManager<AppUser> signInManager = null)
        {
            _configuration = configuration;
            _notify = notyfService;
            _context = context;
            _fileProvider = fileProvider;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        [Authorize(Roles = "Administrator,AltUye")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Administrator,AltUye")]
        public IActionResult ArabaMarka()
		{
			return View();
		}
        [Authorize(Roles = "Administrator,AltUye")]
        public IActionResult ArabaGetir()
		{
			var arabagetir = _context.Markas.Select(x => new MarkaViewModel()
			{
				id = x.id,
				ArabaMarka = x.ArabaMarka,
			}).ToList();
			return Json(arabagetir);
		}
        [Authorize(Roles = "Administrator,AltUye")]
        public IActionResult ArabaGuncelle(MarkaViewModel model)
		{
			var update = _context.Markas.SingleOrDefault(x => x.id == model.id);
			update.ArabaMarka = model.ArabaMarka;
			_context.Markas.Update(update!);
			_context.SaveChanges();
			return Json(true);
		}
        [Authorize(Roles = "Administrator,AltUye")]
        public IActionResult ArabaSil(MarkaViewModel model)
		{
			var sil = _context.Markas.FirstOrDefault(x => x.id == model.id);
			_context.Markas.Remove(sil!);
			_context.SaveChanges();
			return Json(true);
		}
        [Authorize(Roles = "Administrator,AltUye")]
        public IActionResult ArabaMarkEkle(MarkaViewModel model)
		{
			var arabaekle = new Marka();
			arabaekle.ArabaMarka = model.ArabaMarka;
			_context.Markas.Add(arabaekle);
			_context.SaveChanges();
			return Json(true);
		}
        [Authorize(Roles = "Administrator,AltUye")]
        public IActionResult ArabaEkle(ArabaViewModel model)
		{
			var arabalarım = _context.Arabas.Select(x => new ArabaViewModel()
			{
				id = x.id,
				arabadi = x.arabadi,
				arabamotor = x.arabamotor,
				arabakm = x.arabakm,
				arabayakıt = x.arabayakıt,
				ResimData = x.Resim,
			}).ToList();
			return View(arabalarım);
		}




        [Authorize(Roles = "Administrator,AltUye")]
        public IActionResult ArabaYeniEkle()
		{

			List<SelectListItem> yakıt = (from x in _context.Markas.ToList()
										  select new SelectListItem
										  {
											  Text = x.ArabaMarka,
											  Value = x.id.ToString(),

										  }).ToList();
			ViewBag.yakıt = yakıt;
			return View();
		}
        [Authorize(Roles = "Administrator,AltUye")]
        [HttpPost]
		public IActionResult ArabaYeniEkle(ArabaViewModel model)
		{
			var rootFolder = _fileProvider.GetDirectoryContents("wwwroot");
			var photoUrl = "-";
			if (model.Resim.Length > 0 && model.Resim != null)
			{
				var filename = Guid.NewGuid().ToString() + Path.GetExtension(model.Resim.FileName);
				var photoPath = Path.Combine(rootFolder.First(x => x.Name == "Araba").PhysicalPath, filename);
				using var stream = new FileStream(photoPath, FileMode.Create);
				model.Resim.CopyTo(stream);
				photoUrl = filename;
			}

			var araba = new Araba();
			araba.arabadi = model.arabadi;
			araba.arabamotor = model.arabamotor;
			araba.arabakm = model.arabakm;
			araba.arabavitestipi = model.arabavitestipi;
			araba.arabayakıt = model.arabayakıt;
			araba.arabaacıklama = model.arabaacıklama;
			araba.fiyat = model.fiyat;
			araba.markaid = model.markaid;
			araba.Resim = photoUrl;


			_context.Add(araba);
			_context.SaveChanges();
			_notify.Success("Yeni Araç Eklendi");
			return RedirectToAction("ArabaEkle", "Admin");
		}

        [Authorize(Roles = "Administrator,AltUye")]
        [HttpGet]
		public IActionResult ArabaDetail(int id)
		{
			List<SelectListItem> yakıt = (from x in _context.Markas.ToList()
										  select new SelectListItem
										  {
											  Text = x.ArabaMarka,
											  Value = x.id.ToString(),

										  }).ToList();
			ViewBag.yakıt = yakıt;
			var aracım = _context.Arabas
				.Include(x => x.Marka)
				.FirstOrDefault(x => x.id == id);


			var aracımmodel = new ArabaViewModel()
			{
				id = aracım.id,
				arabadi = aracım.arabadi,
				arabakm = aracım.arabakm,
				arabamotor = aracım.arabamotor,
				arabavitestipi = aracım.arabavitestipi,
				arabayakıt = aracım.arabayakıt,
				arabaacıklama = aracım.arabaacıklama,
				fiyat = aracım.fiyat,
				markaid = aracım.markaid,
				ResimData = aracım.Resim,
			};

			ViewBag.resim = aracımmodel.ResimData;
			return View(aracımmodel);




		}
        [Authorize(Roles = "Administrator,AltUye")]
        [HttpPost]
		public IActionResult ArabaDetail(ArabaViewModel model)
		{


			var rootFolder = _fileProvider.GetDirectoryContents("wwwroot");
			var updatedetail = _context.Arabas.SingleOrDefault(x => x.id == model.id);
			updatedetail.arabadi = model.arabadi;
			updatedetail.arabakm = model.arabakm;
			updatedetail.arabamotor = model.arabamotor;
			updatedetail.arabayakıt = model.arabayakıt;
			updatedetail.arabaacıklama = model.arabaacıklama;
			updatedetail.fiyat = model.fiyat;
			updatedetail.markaid = model.markaid;

			if (model.Resim != null && model.Resim.Length > 0)
			{
				// Image processing logic
				var filename = Guid.NewGuid().ToString() + Path.GetExtension(model.Resim.FileName);
				var photoPath = Path.Combine(rootFolder.First(x => x.Name == "Araba").PhysicalPath, filename);

				using var stream = new FileStream(photoPath, FileMode.Create);
				model.Resim.CopyTo(stream);

				updatedetail.Resim = filename;
			}

			_context.Arabas.Update(updatedetail);
			_context.SaveChanges();
			_notify.Success("Araba Güncelleme İşlemi Başarılı Bir Şekilde Gerçekleştirildi");
			return RedirectToAction("ArabaEkle", "Admin");

		}
        [Authorize(Roles = "Administrator,AltUye")]
        public IActionResult ArabaDelete(int id)
		{
			var sil = _context.Arabas.FirstOrDefault(x => x.id == id);
			_context.Arabas.Remove(sil!);
			_context.SaveChanges();
			return RedirectToAction("ArabaEkle", "Admin");
		}
       
       
        [Authorize(Roles = "Administrator,AltUye")]
        public async Task<IActionResult> Kullanıcılar()
        {
            var userModels = await _userManager.Users.Select(x => new UserModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                UserName = x.UserName,
                City = x.City
            }).ToListAsync();
            return View(userModels);
        }
        [Authorize(Roles = "Administrator,AltUye")]
        public async Task<IActionResult> Roller()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
       
        [Authorize(Roles = "Administrator,AltUye")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

    }
}
