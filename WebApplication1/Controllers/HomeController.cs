using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.ModelViews;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notify;
        private readonly AppDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, INotyfService notify, IFileProvider fileProvider, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, AppDbContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _notify = notify;
            _fileProvider = fileProvider;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var arabalarım = _context.Arabas.Select(x => new ArabaViewModel()
            {
                id = x.id,
                arabadi = x.arabadi,
                arabaacıklama = x.arabaacıklama,
                arabamotor = x.arabamotor,
                arabakm = x.arabakm,
                arabayakıt = x.arabayakıt,
                ResimData = x.Resim,
            }).ToList();
            return View(arabalarım);

        }
        [HttpGet]
        public IActionResult ArabaBul(int id)
        {
            var arabalarım = _context.Arabas.Where(x => x.id == id).Select(x => new ArabaViewModel()
            {
                id = x.id,
                arabadi = x.arabadi,
                arabaacıklama = x.arabaacıklama,
                arabamotor = x.arabamotor,
                arabakm = x.arabakm,
                arabayakıt = x.arabayakıt,
                ResimData = x.Resim,
                fiyat=x.fiyat
            }).ToList();
            return View(arabalarım);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> CikisYap()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}