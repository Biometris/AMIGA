﻿using Biometris.Statistics.Distributions;
using Biometris.Statistics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometris.Tests.Statistics.Distributions {

    [TestClass]
    public class PowerLawDistributionTests {

        [TestMethod]
        public void PowerLawDistributionTest_Mean() {
            var distribution = new PowerLawDistribution();
            var samples = Enumerable.Range(1, 10000).Select(r => distribution.Draw()).ToList();
            var mean = samples.Average();
            var stderr = samples.StdErr();
            Assert.AreEqual(mean, distribution.Mean(), stderr);
        }

        [TestMethod]
        public void PowerLawDistributionTest_Variance() {
            var distribution = new PowerLawDistribution();
            var samples = Enumerable.Range(1, 10000).Select(r => distribution.Draw()).ToList();
            var variance = samples.Variance();
            var stderr = samples.StdErr();
            Assert.AreEqual(variance, distribution.Variance(), stderr);
        }

        [TestMethod]
        public void PowerLawDistributionTest_CV() {
            var distribution = new PowerLawDistribution();
            var samples = Enumerable.Range(1, 10000).Select(r => distribution.Draw()).ToList();
            var cv = samples.CV();
            var stderr = samples.StdErr();
            var actual = distribution.CV();
            Assert.AreEqual(cv, actual, stderr);
        }

        [TestMethod]
        public void PowerLawDistributionTest_FromMuCv() {
            var mu = 10;
            var cv = .15;
            var power = 1.5;
            var distribution = PowerLawDistribution.FromMeanCv(mu, cv, power);
            var samples = Enumerable.Range(1, 10000).Select(r => distribution.Draw()).ToList();
            var measuredCv = samples.CV();
            Assert.AreEqual(cv, measuredCv, 1e-2);
        }


    }
}
