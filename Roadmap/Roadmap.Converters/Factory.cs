namespace Converters
{
    using System.Collections.Generic;
    using System.Linq;
    using RoadmapData.Models;

    using ModelRoadmap = RoadmapModels.Roadmap;

    public interface IDataFactory
    {
        IEnumerable<ModelRoadmap> GetAllRoadmaps();
    }

    public class DataFactory : IDataFactory
    {
        private RoadmapContext DatabaseContext { get; }

        public DataFactory(RoadmapContext context)
        {
            this.DatabaseContext = context;
        }

        public IEnumerable<ModelRoadmap> GetAllRoadmaps()
        {
            var roadmaps = 
                from r in this.DatabaseContext.Roadmap
                    select r.ToModel();

            return roadmaps.AsEnumerable();
        }
    }
}
