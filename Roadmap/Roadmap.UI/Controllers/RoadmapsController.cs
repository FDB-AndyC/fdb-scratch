// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Roadmap.UI.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Converters;
    using Models;

    [Route("api/[controller]")]
    public class RoadmapsController : Controller
    {
        private IDataFactory DataFactory { get; }

        public RoadmapsController(IDataFactory dataFactory)
        {
            this.DataFactory = dataFactory;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public IEnumerable<Roadmap> GetAll()
        {
            return this.DataFactory.GetAllRoadmaps();
        }

        [HttpGet("[action]")]
        public Roadmap GetRoadmap(Guid id)
        {
            return this.DataFactory.GetRoadmap(id);
        }
    }
}
