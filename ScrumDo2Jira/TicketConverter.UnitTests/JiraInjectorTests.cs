// -----------------------------------------public ---------------------------------------------------------------------------
// <copyright file="JiraInjectorTests.cs" company="Map Of Medicine">
//   Copyright (c) 2016 Map Of Medicine. All rights reserved.
// </copyright>
// <summary>
//   Defines the JiraInjectorTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TicketConverter.UnitTests
{
    using JIRAInjector;

    using NUnit.Framework;

    [TestFixture]
    public class JiraInjectorTests
    {
        [Test]
        public void WhenCreatingThenTheCorrectKeyIsUsed()
        {
            const string Key = "MDY";
            var injector = new JiraInjector(Key);

            Assert.AreEqual(Key, injector.ProjectKey, "Unexpected project key.");
        }
    }
}
