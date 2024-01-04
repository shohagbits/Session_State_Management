using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Login_Auth.Controllers
{
    [Authorize]
    public class BranchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
