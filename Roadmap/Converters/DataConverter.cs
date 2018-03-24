namespace Converters
{
    using ModelRoadmap = RoadmapModels.Roadmap;
    using DataRoadmap = RoadmapData.Models.Roadmap;
    using ModelCategory = RoadmapModels.Category;
    using DataCategory = RoadmapData.Models.Category;
    using ModelSwimlane = RoadmapModels.Swimlane;
    using DataSwimlane = RoadmapData.Models.Swimlane;

    public static class DataConverter
    {
        public static ModelRoadmap ToModel(this DataRoadmap dataModel)
        {
            return new ModelRoadmap
            {
                Id = dataModel.Id,
                Name = dataModel.Name,
                Description = dataModel.Description,
                StartDate = dataModel.StartDate,
                EndDate = dataModel.EndDate,

                CategoryId = dataModel.CategoryId,
                SwimlaneId = dataModel.SwimlaneId
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
                SortOrder = dataModel.SortOrder
            };
        }
    }
}
