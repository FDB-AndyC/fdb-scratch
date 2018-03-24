using System;
using System.Collections.Generic;

namespace Roadmap.Models
{
    public partial class Swimlane
    {
        public Swimlane()
        {
            Deliverable = new HashSet<Deliverable>();
        }

        public Guid Id { get; set; }
        public Guid RoadmapId { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }

        public Roadmap Roadmap { get; set; }
        public ICollection<Deliverable> Deliverable { get; set; }
    }
}
