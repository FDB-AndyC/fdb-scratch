// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScrumdoExtractorTests.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the ScrumdoExtractorTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TicketConverter.UnitTests
{
    using System.Linq;

    using Contracts;

    using Moq;

    using NUnit.Framework;

    using ScrumDoExtractor;

    [TestFixture]
    public class ScrumdoExtractorTests
    {
        [Test]
        public void WhenInitialisingThenGetAnObject()
        {
            var reader = new ScrumdoExtractor();
            Assert.IsNotNull(reader, "No extractor object.");
        }

        [Test]
        public void WhenInitialisingThenItIsAnITicketReader()
        {
            var reader = new ScrumdoExtractor() as ITicketReader;
            Assert.IsNotNull(reader, "Extractor not created or not the right interface type.");
        }

        [Test]
        public void WhenListingOrganisationsThenHearstHealthInternationalDescriptionIsReturned()
        {
            var reader = new ScrumdoExtractor();

            var organisations = reader.GetOrganisations();
            var hhi = organisations.Single();

            Assert.AreEqual("Hearst Health International", hhi.Description);
        }

        [Test]
        public void WhenListingOrganisationsThenHearstHealthInternationalSlugIsReturned()
        {
            var reader = new ScrumdoExtractor();

            var organisations = reader.GetOrganisations();
            var hhi = organisations.Single();

            Assert.AreEqual("fdbe", hhi.ShortName);
        }

        [Test]
        public void WhenListingProjectsThenMedsOptIsReturned()
        {
            var reader = new ScrumdoExtractor();
            var mockOrganisation = new Mock<IOrganisation>();

            mockOrganisation
                .Setup(o => o.ShortName)
                .Returns("fdbe");

            var projects = reader.GetProjects(mockOrganisation.Object);
            var medsopt = projects.Single(p => p.ShortName.Equals("medsopt"));

            Assert.AreEqual("Meds Opt. Software", medsopt.LongName, "Unexpected project long name.");
        }

        [Test]
        public void WhenListingStoriesPerProjectThenTheHistoricVelocityStoryIsIncluded()
        {
            var reader = new ScrumdoExtractor();
            var mockProject = new Mock<IProject>();
            var mockOrganisation = new Mock<IOrganisation>();

            mockOrganisation
                .Setup(o => o.ShortName)
                .Returns("fdbe");

            mockProject
                .Setup(p => p.ShortName)
                .Returns("medsopt");

            mockProject
                .Setup(p => p.Organisation)
                .Returns(mockOrganisation.Object);

            var stories = reader.GetStories(mockProject.Object);
            var truth = stories.Single(s => s.Summary.Equals("Historic velocity"));

            Assert.AreEqual(string.Empty, truth.Detail, "Unexpected description for \"truth\" story.");
        }

        [Test]
        public void WhenListingIterationsThenTheBacklogIsReturned()
        {
            var reader = new ScrumdoExtractor();
            var mockProject = new Mock<IProject>();
            var mockOrganisation = new Mock<IOrganisation>();

            mockOrganisation
                .Setup(o => o.ShortName)
                .Returns("fdbe");

            mockProject
                .Setup(p => p.ShortName)
                .Returns("medsopt");

            mockProject
                .Setup(p => p.Organisation)
                .Returns(mockOrganisation.Object);

            var iterations = reader.GetIterations(mockProject.Object);
            var backlog = iterations.Single(i => i.IterationType == 0);

            Assert.IsTrue(backlog.IsDefault, "Unexpected backlog default flag.");
            Assert.AreEqual("Backlog", backlog.Name, "Unexpected backlog name.");
        }
    }
}
