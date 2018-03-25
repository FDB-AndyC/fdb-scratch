using System;
using System.Collections.Generic;

namespace Roadmap.Data.Models
{
    public partial class Category
    {
        public Category()
        {
            Deliverable = new HashSet<Deliverable>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? ColourIndex { get; set; }

        public ICollection<Deliverable> Deliverable { get; set; }
    }
}
