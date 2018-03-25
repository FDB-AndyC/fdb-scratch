using System;
using System.Collections.Generic;
using System.Text;

namespace RoadmapModels
{
    public class Roadmap
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Swimlane> Swimlanes { get; set; }
    }
}
