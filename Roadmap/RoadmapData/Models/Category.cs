using System;
using System.Collections.Generic;

namespace RoadmapData.Models
{
    public partial class Category
    {
        public Category()
        {
            Roadmap = new HashSet<Roadmap>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? ColourIndex { get; set; }

        public ICollection<Roadmap> Roadmap { get; set; }
    }
}
