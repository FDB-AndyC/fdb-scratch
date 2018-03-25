using System.Collections.ObjectModel;
using System.Linq;

namespace Converters
{
    using ModelRoadmap = RoadmapModels.Roadmap;
    using DataRoadmap = RoadmapData.Models.Roadmap;
    using ModelCategory = RoadmapModels.Category;
    using DataCategory = RoadmapData.Models.Category;
    using ModelSwimlane = RoadmapModels.Swimlane;
    using DataSwimlane = RoadmapData.Models.Swimlane;
    using ModelDeliverable = RoadmapModels.Deliverable;
    using DataDeliverable = RoadmapData.Models.Deliverable;

    public static class DataConverter
    {
        public static ModelRoadmap ToModel(this DataRoadmap dataModel)
        {
            return new ModelRoadmap
            {
                Id = dataModel.Id,
                Name = dataModel.Name,
                Description = dataModel.Description
            };
        }

        public static ModelCategory ToModel(this DataCategory dataModel)
        {
            return new ModelCategory
            {
                Id = dataModel.Id,
                Name = dataModel.Name,
                ColourIndex = dataModel.ColourIndex
            };
        }

        public static ModelSwimlane ToModel(this DataSwimlane dataModel)
        {
            return new ModelSwimlane
            {
                Id = dataModel.Id,
                Name = dataModel.Name,
                SortOrder = dataModel.SortOrder,
                Deliverables = new Collection<ModelDeliverable>(dataModel.Deliverable.Select(dd => dd.ToModel()).ToList())
            };
        }

        public static ModelDeliverable ToModel(this DataDeliverable dataModel)
        {
            return new ModelDeliverable
            {
                Id = dataModel.Id,
                Name = dataModel.Name,
                Description = dataModel.Description,
                StartDate = dataModel.StartDate,
                EndDate = dataModel.EndDate,
                Category = dataModel.Category.ToModel()
            };
        }
    }
}
