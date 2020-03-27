using System.Linq;
using ExcelCellTranslator;
using NUnit.Framework;

namespace ExcelCellTranslatorTests
{
    public class ArgumentsExtractorTests
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void GivenDatabaseConnectionStringWhenExtractingThenItIsReturned()
        {
            var processed = ProcessedArguments.Process(new[] {"one", "two", "-db=connectionstring"});
            var connectionString = ArgumentsExtractor.ExtractDatabaseConnectionString(ref processed);

            Assert.AreEqual("connectionstring", connectionString, "Unexpected Connection String returned");
        }

        [Test]
        public void GivenDatabaseConnectionStringWhenExtractingThenItIsRemovedFromSwitches()
        {
            var processed = ProcessedArguments.Process(new[] { "one", "two", "-db=connectionstring" });
            var connectionString = ArgumentsExtractor.ExtractDatabaseConnectionString(ref processed);

            Assert.IsFalse(processed.Switches.Any());
        }
    }
}
