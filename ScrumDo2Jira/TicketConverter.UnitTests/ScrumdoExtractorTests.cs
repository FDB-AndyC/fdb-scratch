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
    using Contracts;

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
        public void WhenInitialisingThenCanConnect()
        {
            var reader = new ScrumdoExtractor();

            reader.Connect();
        }

        [Test]
        public void WhenListingTicketsThenTheyAreReturned()
        {
            var reader = new ScrumdoExtractor();

            reader.Connect();
        }
    }
}
