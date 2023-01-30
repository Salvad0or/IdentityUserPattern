using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    [Authorize(Policy = "Manager")]
    public class PolicyManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
