﻿// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Roadmap.UI.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Converters;
    using Models;

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
        public IEnumerable<Roadmap> GetAll()
        {
            return this.DataFactory.GetAllRoadmaps();
        }
    }
}
