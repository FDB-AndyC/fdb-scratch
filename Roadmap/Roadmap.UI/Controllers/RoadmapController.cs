using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Roadmap.UI.Controllers
{
    public class RoadmapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}