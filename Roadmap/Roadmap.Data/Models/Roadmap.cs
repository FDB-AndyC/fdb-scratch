using System;
using System.Collections.Generic;

namespace Roadmap.Data.Models
{
    public partial class Roadmap
    {
        public Roadmap()
        {
            Milestone = new HashSet<Milestone>();
            Swimlane = new HashSet<Swimlane>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Milestone> Milestone { get; set; }
        public ICollection<Swimlane> Swimlane { get; set; }
    }
}
