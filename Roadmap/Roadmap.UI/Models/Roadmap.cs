using System;
using System.Collections.Generic;

namespace Roadmap.Models
{
    public partial class Roadmap
    {
        public Roadmap()
        {
            Deliverable = new HashSet<Deliverable>();
            Swimlane = new HashSet<Swimlane>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Deliverable> Deliverable { get; set; }
        public ICollection<Swimlane> Swimlane { get; set; }
    }
}
