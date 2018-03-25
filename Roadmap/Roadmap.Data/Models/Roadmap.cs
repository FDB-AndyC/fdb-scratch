using System;
using System.Collections.Generic;

namespace RoadmapData.Models
{
    public partial class Roadmap
    {
        public Roadmap()
        {
            Swimlane = new HashSet<Swimlane>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Swimlane> Swimlane { get; set; }
    }
}
