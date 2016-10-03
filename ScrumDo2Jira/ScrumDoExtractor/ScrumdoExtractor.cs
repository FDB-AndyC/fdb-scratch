// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScrumdoExtractor.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the ScrumdoExtractor type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ScrumDoExtractor
{
    using System.Collections.Generic;
    using System.Net;

    using Contracts;

    using Core;

    using Newtonsoft.Json;

    public class ScrumdoExtractor : WebClient, ITicketReader
    {
        public ScrumdoExtractor()
        {
            this.BaseAddress = "https://www.scrumdo.com/api/v2";
            this.Credentials = new NetworkCredential("acollyer@fdbhealth.com", "Make1ts0+scrumdo");
        }

        public IEnumerable<IOrganisation> GetOrganisations()
        {
            var json = this.DownloadString(this.GetRelativeAddress("/organizations/"));
            return DynamicExtensions.ToArrayOf<ScrumdoOrganisation>(
                JsonConvert.DeserializeObject(json),
                (d) => ScrumdoOrganisation.Create(d));
        }

        public IEnumerable<IProject> GetProjects(IOrganisation organisation)
        {
            var json =
                this.DownloadString(
                    this.GetRelativeAddress($"/organizations/{organisation?.ShortName ?? string.Empty}/projects"));

            return DynamicExtensions.ToArrayOf<ScrumdoProject>(
                JsonConvert.DeserializeObject(json),
                (p) => ScrumdoProject.Create(organisation, p));
        }

        public IEnumerable<IStory> GetStories(IProject project)
        {
            var json =
                this.DownloadString(
                    this.GetRelativeAddress($"/organizations/{project?.Organisation?.ShortName ?? string.Empty}/projects/{project?.ShortName ?? string.Empty}/stories"));

            var stories = JsonConvert.DeserializeObject<ScrumdoStoryContainer>(json);

            return DynamicExtensions.ToArrayOf<ScrumdoStory>(
                stories.items,
                (s) => ScrumdoStory.Create(project, s));
        }

        public IEnumerable<IIteration> GetIterations(IProject project)
        {
            var json =
                this.DownloadString(
                    this.GetRelativeAddress($"/organizations/{project?.Organisation?.ShortName ?? string.Empty}/projects/{project?.ShortName ?? string.Empty}/iterations"));

            return DynamicExtensions.ToArrayOf<ScrumdoIteration>(
                JsonConvert.DeserializeObject(json),
                (i) => ScrumdoIteration.Create(project, i));
        }

        private string GetRelativeAddress(string suffix)
        {
            return $"{this.BaseAddress}{suffix}";
        }
    }
}
