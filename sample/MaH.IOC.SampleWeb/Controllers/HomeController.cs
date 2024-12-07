using MaH.IOC.SampleWeb.Services;
using System.Web.Mvc;

namespace MaH.IOC.SampleWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IScopedService _scopedService;

        public HomeController(IScopedService scopedService)
        {
            _scopedService = scopedService;
        }

        public ActionResult Index()
        {
            var model = _scopedService.GetValues();
            return View(model);
        }
    }
}
