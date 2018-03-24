// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Roadmap.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using Converters;
    using RoadmapModels;

    [Route("api/[controller]")]
    public class RoadmapController : Controller
    {
        private IDataFactory DataFactory { get; }

        public RoadmapController(IDataFactory dataFactory)
        {
            this.DataFactory = dataFactory;
        }

        ////// GET: /<controller>/
        ////public IActionResult Index()
        ////{
        ////    return View();
        ////}

        [HttpGet("[action]")]
        public IEnumerable<Roadmap> Roadmaps()
        {
            return this.DataFactory.GetAllRoadmaps();
        }
    }
}
