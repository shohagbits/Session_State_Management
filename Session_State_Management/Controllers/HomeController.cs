using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Session_State_Management.Models;
using System.Diagnostics;
using System.Text;

namespace Session_State_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDistributedCache _cache;

        public HomeController(ILogger<HomeController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Set()
        {
            var testValue = "Test Shohag";
            byte[] encodedTestValue = Encoding.UTF8.GetBytes(testValue);
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(20));
            await _cache.SetAsync("Test", encodedTestValue, options);

            return RedirectToAction("Get");
        }
        public async Task<IActionResult> Get()
        {
            var encodedTest = await _cache.GetAsync("Test");

            if (encodedTest != null)
            {
                ViewBag.Test = Encoding.UTF8.GetString(encodedTest);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}