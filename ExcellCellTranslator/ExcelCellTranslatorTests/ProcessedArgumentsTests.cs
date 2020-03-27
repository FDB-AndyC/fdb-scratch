using System;
using System.Linq;
using ExcelCellTranslator;
using NUnit.Framework;

namespace ExcelCellTranslatorTests
{
    public class ProcessedArgumentsTests
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void GivenNoArgumentsThenNoFileNamesAreReturned()
        {
            var inputs = new string[] {};
            var processed = ProcessedArguments.Process(inputs);

            Assert.IsFalse(processed.Filenames.Any(), "Expecting no file names");
        }
        
        [Test]
        public void GivenNoArgumentsThenNoSwitchesAreReturned()
        {
            var inputs = new string[] { };
            var processed = ProcessedArguments.Process(inputs);

            Assert.IsFalse(processed.Switches.Any(), "Expecting no switches");
        }

        [Test]
        public void GivenNoSwitchesThenAllArgumentsAssignedAsFileNames()
        {
            var inputs = new[] {"first", "second", "third"};
            var processed = ProcessedArguments.Process(inputs);

            Assert.AreEqual(inputs.Length, processed.Filenames.Count, "Unexpected number of file names");
        }

        [Test]
        public void WhenProcessingThenAllFileNamesAppearInOrder()
        {
            var inputs = new[] { "first", "second", "third" };
            var processed = ProcessedArguments.Process(inputs);

            Assert.AreEqual("first", processed.Filenames.First(), "Unexpected first file name");
            Assert.AreEqual("second", processed.Filenames.Skip(1).First(), "Unexpected second file name");
            Assert.AreEqual("third", processed.Filenames.Last(), "Unexpected last file name");
        }

        [Test]
        public void GivenAllSwitchesThenNoFileNamesAreAssigned()
        {
            var inputs = new[] {"-first", "--second", "/third"};
            var processed = ProcessedArguments.Process(inputs);

            Assert.IsFalse(processed.Filenames.Any(), "Expecting no file names");
        }

        [Test]
        public void GivenSwitchesAndFileNamesThenCorrectNumberOfSwitchesAreAssigned()
        {
            var inputs = new[] { "-switch1", "FirstFile", "--switch2", "SecondFile", "/thirdswitch" };
            var processed = ProcessedArguments.Process(inputs);

            Assert.AreEqual(3, processed.Switches.Count, "Unexpected number of switches");
        }

        [Test]
        public void GivenSwitchesAndFileNamesThenCorrectNumberOfFileNamesAreAssigned()
        {
            var inputs = new[] { "-switch1", "FirstFile", "--switch2", "SecondFile", "/thirdswitch" };
            var processed = ProcessedArguments.Process(inputs);

            Assert.AreEqual(2, processed.Filenames.Count, "Unexpected number of file names");
        }

        [Test]
        public void GivenSwitchesAndFileNamesThenCorrectOrderOfFileNamesAreAssigned()
        {
            var inputs = new[] { "-switch1", "C:\\Path\\To\\First.File", "--switch2", "\\\\Network\\Path\\To\\Second.File", "/thirdswitch" };
            var processed = ProcessedArguments.Process(inputs);

            Assert.AreEqual("C:\\Path\\To\\First.File", processed.Filenames.First(), "Unexpected first file name");
            Assert.AreEqual("\\\\Network\\Path\\To\\Second.File", processed.Filenames.Last(), "Unexpected last file name");
        }

        [Test]
        public void WhenTestingForSwitchPresenceThenAllSwitchesHit()
        {
            var inputs = new[] { "-switch1", "C:\\Path\\To\\First.File", "--switch2", "\\\\Network\\Path\\To\\Second.File", "/thirdswitch" };
            var processed = ProcessedArguments.Process(inputs);

            Assert.IsTrue(processed.ContainsSwitch("switch1"));
            Assert.IsTrue(processed.ContainsSwitch("switch2"));
            Assert.IsTrue(processed.ContainsSwitch("thirdswitch"));
        }

        [Test]
        public void WhenTestingForSwitchPresenceThenNoFalsePositives()
        {
            var inputs = new[] { "-switch1", "C:\\Path\\To\\First.File", "--switch2", "\\\\Network\\Path\\To\\Second.File", "/thirdswitch" };
            var processed = ProcessedArguments.Process(inputs);

            Assert.IsFalse(processed.ContainsSwitch(string.Empty), "Unexpected false positive");
            Assert.IsFalse(processed.ContainsSwitch(null), "Unexpected false positive");
            Assert.IsFalse(processed.ContainsSwitch("anotherswitch"), "Unexpected false positive");
        }

        [Test]
        public void WhenProcessingThenSwitchesRetainCasing()
        {
            var inputs = new[] {"--One", "/TWO", "-three"};
            var processed = ProcessedArguments.Process(inputs);

            Assert.IsTrue(string.Compare("One", processed.Switches.First(), StringComparison.Ordinal) == 0, "Expecting switches to be made lower case");
            Assert.IsTrue(string.Compare("TWO", processed.Switches.Skip(1).First(), StringComparison.Ordinal) == 0, "Expecting switches to be made lower case");
            Assert.IsTrue(string.Compare("three", processed.Switches.Last(), StringComparison.Ordinal) == 0, "Expecting switches to be made lower case");
        }

        [Test]
        public void GivenSingleInvalidSwitchThenNoFileNamesAndNoSwitchesAreAssigned()
        {
            var inputs = new[] {"---"};
            var processed = ProcessedArguments.Process(inputs);

            Assert.IsFalse(processed.Switches.Any(), "Not expecting any switches");
            Assert.IsFalse(processed.Filenames.Any(), "Not expecting any file names");
        }
    }
}
