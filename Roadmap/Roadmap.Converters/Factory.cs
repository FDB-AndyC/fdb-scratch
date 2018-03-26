using System;

namespace Roadmap.Converters
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;

    using ModelRoadmap = Models.Roadmap;

    public interface IDataFactory
    {
        IEnumerable<ModelRoadmap> GetAllRoadmaps();

        ModelRoadmap GetRoadmap(Guid id);
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

        public ModelRoadmap GetRoadmap(Guid id)
        {
            var dataRoadmap =
                (from r in this.DatabaseContext.Roadmap
                    where r.Id.Equals(id)
                    select r)
                .SingleOrDefault();

            return dataRoadmap.ToModel();
        }
    }
}
