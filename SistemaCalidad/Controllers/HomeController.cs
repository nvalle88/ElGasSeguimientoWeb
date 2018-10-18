#region Using

using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ElGasSeguimientoWeb.Models;

#endregion

namespace ElGasSeguimientoWeb.Controllers
{
    [Authorize(Policy = "Distribuidores")]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        [Route("dashboard-marketing")]
        public IActionResult DashboardMarketing() => View();

        [Route("dashboard-social")]
        public IActionResult SocialWall() => View();

        public IActionResult Inbox() => View();

        public IActionResult Chat() => View();

        public IActionResult Widgets() => View();

        public IActionResult Error() => View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
