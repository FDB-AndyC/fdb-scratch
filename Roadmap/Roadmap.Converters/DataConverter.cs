namespace Roadmap.Converters
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using ModelRoadmap = Models.Roadmap;
    using DataRoadmap = Data.Models.Roadmap;
    using ModelCategory = Models.Category;
    using DataCategory = Data.Models.Category;
    using ModelSwimlane = Models.Swimlane;
    using DataSwimlane = Data.Models.Swimlane;
    using ModelDeliverable = Models.Deliverable;
    using DataDeliverable = Data.Models.Deliverable;

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
