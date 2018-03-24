using System;
using System.Collections.Generic;

namespace RoadmapData.Models
{
    public partial class Swimlane
    {
        public Swimlane()
        {
            Roadmap = new HashSet<Roadmap>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }

        public ICollection<Roadmap> Roadmap { get; set; }
    }
}
