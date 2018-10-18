#region Using

using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ElGasSeguimientoWeb.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

#endregion

namespace ElGasSeguimientoWeb.Controllers
{
    [Authorize(Policy = "Distribuidores")]
    public class HomeController : Controller
    {

        public IActionResult Index() => View();
        public IActionResult Error() => View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
