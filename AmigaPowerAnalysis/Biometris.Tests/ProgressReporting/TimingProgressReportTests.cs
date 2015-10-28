using System;
using System.Threading;
using Biometris.ProgressReporting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biometris.Test.UnitTests {
    [TestClass]
    public class TimingProgressReportTests {

        /// <summary>
        /// Initialization test.
        /// </summary>
        [TestMethod]
        [TestCategory("UnitTests")]
        public void TimingProgressReportTest1() {
            var progressReport = new TimingProgressReport();
            Assert.AreEqual(0, progressReport.Elapsed.Ticks);
            Assert.AreEqual(TimeSpan.MaxValue, progressReport.Remaining);
            Assert.AreEqual(string.Empty, progressReport.CurrentActivity);
            Assert.AreEqual(0D, progressReport.Progress);
        }

        /// <summary>
        /// Test high number of progress states as substates, check for 100% progress after the loop.
        /// </summary>
        [TestMethod]
        [TestCategory("UnitTests")]
        public void TimingProgressReportTest2() {
            var progressReport = new TimingProgressReport();
            Assert.AreEqual(0, progressReport.Elapsed.Ticks);
            Assert.AreEqual(TimeSpan.MaxValue, progressReport.Remaining);
            var numberOfStates = 10;
            var incrementAmount = 100D / numberOfStates;
            for (int i = 0; i < numberOfStates; i++) {
                var subState = progressReport.NewProgressState(100);
                subState.Increment(string.Format("State {0}", i), incrementAmount);
                Thread.Sleep(10);
            }
            Assert.AreEqual(0, progressReport.Remaining.Ticks);
        }
    }
}
