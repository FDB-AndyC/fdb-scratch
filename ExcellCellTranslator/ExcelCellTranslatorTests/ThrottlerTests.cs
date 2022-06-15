using System;
using System.Collections.Generic;
using ExcelCellTranslator;
using Moq;
using NUnit.Framework;
using ThrottlingService;

namespace ExcelCellTranslatorTests
{
    public class ThrottlerTests
    {
        private int ExecuteCallCounter;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GivenNoHistoryWhenExecutingThenItHappensImmediately()
        {
            var mockDelayer = new Mock<IDelayer>();
            var throttler = new ThroughputThrottler(new UtcClock(), mockDelayer.Object);
            var texts = new List<string>(new[] {"one", "two", "three"});

            mockDelayer
                .Setup(d => d.Delay(It.IsAny<TimeSpan>()))
                .Callback(() => Assert.Fail("Not expecting to receive a call to delay"));

            var results = throttler.Execute((x) => texts, texts);
        }

        [Test]
        public void GivenLimitExceededWhenExecutingThenDelayIsCalled()
        {
            var mockDelayer = new Mock<IDelayer>();
            var mockClock = new Mock<IClock>();
            var dummyDate = new DateTime(1974, 3, 14, 5, 30, 18);
            var throttler = new ThroughputThrottler(mockClock.Object, mockDelayer.Object);
            var executeData = new List<string>(new[] {"first", "second", "third"});

            // arrange
            mockClock
                .Setup(c => c.GetTime())
                .Returns(dummyDate);

            mockDelayer
                .Setup(d => d.Delay(It.IsAny<TimeSpan>()))
                .Callback(() => Assert.Fail("Unexpected Delay callback"));

            PopulateThrottler(throttler);

            mockClock
                .Setup(c => c.GetTime())
                .Returns(() => dummyDate + TimeSpan.FromSeconds(++this.ExecuteCallCounter));

            mockDelayer
                .Setup(d => d.Delay(It.IsAny<TimeSpan>()))
                .Verifiable();

            this.ExecuteCallCounter = 0;

            // act
            var results = throttler.Execute(ThrottlerActionFunc, executeData);

            // assert
            mockDelayer.Verify(d => d.Delay(It.IsAny<TimeSpan>()), Times.AtLeastOnce, "Unverified delay execution");
        }

        private  void PopulateThrottler(ThroughputThrottler throttler)
        {
            var texts = new List<string>();

            for (var i = 0; i < 10; ++i)
            {
                texts.Add(new string('a', 100));
            }

            for (var i = 0; i < 100; ++i)
            {
                throttler.Execute(ThrottlerActionFunc, texts);
            }
        }

        private IList<string> ThrottlerActionFunc(IList<string> inputs)
        {
            ++this.ExecuteCallCounter;
            return inputs;
        }
    }
}