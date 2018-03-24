using System;
using System.Collections.Generic;
using System.Text;

namespace RoadmapModels
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? ColourIndex { get; set; }
    }
}
