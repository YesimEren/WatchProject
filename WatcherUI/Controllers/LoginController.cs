using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WatcherAPI.Classes;

namespace WatcherUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            var values = _context.Admins.FirstOrDefault(x => x.Username == username && x.Password == password);

            if (values == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "WatchUI");
            }
        }

        [HttpGet]
        public IActionResult AddUsers()
        {
            return View();
        }
        [HttpPost("Login/AddUsers")]
        public IActionResult AddVirtualMachine(Admin admin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Admins.Add(admin);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "WatchUI");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", "Sanal makine eklenirken bir hata oluştu.");
                    return View(admin);
                }
            }
            return View(admin);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Kullanıcının çıkış yapmasını sağla
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Çıkış yapıldıktan sonra tarayıcı önbelleğinden silme işlemi
            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            // Sayfayı tarayıcı geçmişinde bir sayfa olarak işaretleme
            Response.Headers.Add("Referrer-Policy", "no-referrer");

            // Çıkış yapıldıktan sonra yönlendirilecek sayfayı belirle
            return RedirectToAction("Index", "Login");
        }
    }
}
