using System;
using System.Collections.Generic;
using System.Linq;
using Biometris.Statistics.Histograms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biometris.Test.UnitTests {

    [TestClass]
    public class HistogramBinUtilitiesTests {

        private enum Gender { Male, Female }

        private class Person {
            public string Name { get; set; }
            public int Age { get; set; }
            public double Number { get; set; }
            public List<Gender> Children { get; set; }
            public int NumberOfChildren {
                get {
                    return Children.Count;
                }
            }
            public override string ToString() {
                return string.Format("Name: {0}, Age: {1}", Name, Age);
            }
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeHistogramBinsTest0() {
            List<double> data = new List<double>();
            var bins = data.MakeHistogramBins();
            Assert.AreEqual(0, bins.Count);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeHistogramBinsTest1() {
            List<double> data = new List<double>();
            var bins = data.MakeHistogramBins(4, 1, 4);
            Assert.AreEqual(4, bins.Count);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeHistogramBinsTest2() {
            var random = new Random();
            var persons = new List<Person>();
            for (int i = 0; i < 1000; i++) {
                persons.Add(new Person() {
                    Number = random.NextDouble()
                });
            }
            persons.Add(new Person() {
                Number = 1000,
            });
            var bins = persons.Select(v=>v.Number).MakeHistogramBins(10);
            Assert.IsTrue(bins[0].Frequency == 1000);
            Assert.IsTrue(bins[9].Frequency == 1);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeHistogramBinsTest3() {
            var random = new Random();
            var persons = new List<Person>();

            for (int i = 0; i < 1000; i++) {
                persons.Add(new Person() {
                    Number = random.NextDouble()
                });
            }

            persons.Add(new Person() {
                Number = 1000,
            });

            persons.Add(new Person() {
                Number = -1000,
            });

            var bins = persons.Select(v => v.Number).MakeHistogramBins(10, 0, 1);
            var n = bins.Sum(b => b.Frequency);
            Assert.IsTrue(bins.Sum(b => b.Frequency) == 1000);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeHistogramBinsTest4() {
            var random = new Random();
            var persons = new List<Person>();
            for (int i = 0; i < 1000; i++) {
                persons.Add(new Person() {
                    Number = random.NextDouble()
                });
            }

            persons.Add(new Person() {
                Number = 1000,
            });

            persons.Add(new Person() {
                Number = -1000,
            });

            var bins = persons.Select(v => v.Number).MakeHistogramBins(10, 0, 1,OutlierHandlingMethod.IncludeHigher);
            Assert.IsTrue(bins.Sum(b => b.Frequency) == 1001);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeHistogramBinsTest5() {
            var random = new Random();
            var persons = new List<Person>();
            for (int i = 0; i < 1000; i++) {
                persons.Add(new Person() {
                    Number = random.NextDouble()
                });
            }

            persons.Add(new Person() {
                Number = 1000,
            });

            persons.Add(new Person() {
                Number = -1000,
            });
            var bins = persons.Select(v => v.Number).MakeHistogramBins(10, 0, 1, OutlierHandlingMethod.IncludeLower);
            Assert.IsTrue(bins.Sum(b => b.Frequency) == 1001);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeHistogramBinsTest6() {
            var random = new Random();
            var persons = new List<Person>();
            for (int i = 0; i < 1000; i++) {
                persons.Add(new Person() {
                    Number = random.NextDouble()
                });
            }

            persons.Add(new Person() {
                Number = 1000,
            });

            persons.Add(new Person() {
                Number = -1000,
            });

            var bins = persons.Select(v => v.Number).MakeHistogramBins(10, 0, 1, OutlierHandlingMethod.IncludeBoth);
            Assert.IsTrue(bins.Sum(b => b.Frequency) == 1002);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeHistogramBinsTest7() {
            var random = new Random();
            var persons = new List<Person>();
            for (int i = 0; i < 1000; i++) {
                persons.Add(new Person() {
                    Number = random.NextDouble()
                });
            }
            var bins = persons.Select(v => v.Number).MakeHistogramBins(10, 0, 1, OutlierHandlingMethod.IncludeBoth);
            System.Diagnostics.Trace.WriteLine(bins.Average());
            System.Diagnostics.Trace.WriteLine(bins.Variance());
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeHistogramBinsTest8() {
            var random = new Random();
            List<double> data = new List<double> { 0.1, 0.2 };
            var bins = data.MakeHistogramBins(10);
            Assert.IsTrue(bins.GetTotalFrequency() == 2);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeHistogramBinsTest9() {
            List<double> data = new List<double> { 1 };
            var bins = data.MakeHistogramBins();
            Assert.IsTrue(bins.GetTotalFrequency() == 1);
            Assert.IsTrue(bins.First().XMinValue < 1);
            Assert.IsTrue(bins.Last().XMaxValue > 1);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeHistogramBinsTest10() {
            List<double> data = new List<double> { -1, -1, -1 };
            var bins = data.MakeHistogramBins();
            Assert.IsTrue(bins.First().XMinValue < bins.First().XMaxValue);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeCategorizedHistogramBinsTest0() {
            var persons = new List<Person>();
            Func<Person, List<CategoryContribution<Gender>>> categoryExtractor = (x) => x.Children.GroupBy(c => c).Select(g => new CategoryContribution<Gender>(g.Key, (double)g.Count())).ToList();
            Func<Person, double> valueExtractor = (x) => x.NumberOfChildren;
            var categorizedBins = persons.MakeCategorizedHistogramBins<Person, Gender>(categoryExtractor, valueExtractor);
            Assert.AreEqual(0, categorizedBins.Count);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeCategorizedHistogramBinsTest1() {
            var persons = new List<Person>();
            Func<Person, List<CategoryContribution<Gender>>> categoryExtractor = (x) => x.Children.GroupBy(c => c).Select(g => new CategoryContribution<Gender>(g.Key, (double)g.Count())).ToList();
            Func<Person, double> valueExtractor = (x) => x.NumberOfChildren;
            var categorizedBins = persons.MakeCategorizedHistogramBins<Person, Gender>(categoryExtractor, valueExtractor, 4, 1, 4);
            Assert.AreEqual(4, categorizedBins.Count);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeCategorizedHistogramBinsTest2() {
            var persons = new List<Person>();

            persons.Add(new Person() {
                Number = 0,
                Children = new List<Gender>() { Gender.Male }
            });

            persons.Add(new Person() {
                Number = 0,
                Children = new List<Gender>() { Gender.Male, Gender.Female }
            });

            persons.Add(new Person() {
                Number = 0,
                Children = new List<Gender>() { Gender.Male, Gender.Female, Gender.Female }
            });

            persons.Add(new Person() {
                Number = 0,
                Children = new List<Gender>() { Gender.Male, Gender.Female, Gender.Female }
            });

            persons.Add(new Person() {
                Number = 0,
                Children = new List<Gender>() { Gender.Male, Gender.Male, Gender.Male, Gender.Female }
            });

            persons.Add(new Person() {
                Number = 0,
                Children = new List<Gender>() { Gender.Male, Gender.Female, Gender.Female, Gender.Female }
            });

            var bins = persons.Select(v => (double)v.NumberOfChildren).MakeHistogramBins(4);
            Func<Person, List<CategoryContribution<Gender>>> categoryExtractor = (x) => x.Children.GroupBy(c => c).Select(g => new CategoryContribution<Gender>(g.Key, (double)g.Count())).ToList();
            Func<Person, double> valueExtractor = (x) => x.NumberOfChildren;
            var categorizedBins = persons.MakeCategorizedHistogramBins<Person, Gender>(categoryExtractor, valueExtractor, 4, 0.5, 4.5);
            CollectionAssert.AreEqual(new List<int> { 1, 1, 2, 2 }, categorizedBins.Select(b => b.Frequency).ToList());
            CollectionAssert.AreEqual(new List<double> { 1D }, categorizedBins.ElementAt(0).ContributionFractions.Select(cf => cf.Contribution).OrderBy(cf => cf).ToList());
            CollectionAssert.AreEqual(new List<double> { 0.5, 0.5 }, categorizedBins.ElementAt(1).ContributionFractions.Select(cf => cf.Contribution).OrderBy(cf => cf).ToList());
            CollectionAssert.AreEqual(new List<double> { 1D/3, 2D/3 }, categorizedBins.ElementAt(2).ContributionFractions.Select(cf => cf.Contribution).OrderBy(cf => cf).ToList());
            CollectionAssert.AreEqual(new List<double> { 0.5, 0.5 }, categorizedBins.ElementAt(3).ContributionFractions.Select(cf => cf.Contribution).OrderBy(cf => cf).ToList());
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeCategorizedHistogramBinsTest3() {
            var persons = new List<Person>();
            persons.Add(new Person() {
                Number = 0,
                Children = new List<Gender>() { Gender.Male }
            });
            Func<Person, List<CategoryContribution<Gender>>> categoryExtractor = (x) => x.Children.GroupBy(c => c).Select(g => new CategoryContribution<Gender>(g.Key, (double)g.Count())).ToList();
            Func<Person, double> valueExtractor = (x) => x.NumberOfChildren;
            var categorizedBins = persons.MakeCategorizedHistogramBins<Person, Gender>(categoryExtractor, valueExtractor);
            Assert.IsTrue(categorizedBins.GetTotalFrequency() == 1);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void MakeCategorizedHistogramBinsTest4() {
            var values = new List<int>() { -1, -1, -1 };
            Func<int, List<CategoryContribution<int>>> categoryExtractor = (x) => new List<CategoryContribution<int>> { new CategoryContribution<int>(x, 1D) };
            Func<int, double> valueExtractor = (x) => x;
            var bins = values.MakeCategorizedHistogramBins<int, int>(categoryExtractor, valueExtractor);
            Assert.IsTrue(bins.GetTotalFrequency() == 3);
            Assert.IsTrue(bins.First().XMinValue < bins.First().XMaxValue);
        }
    }
}
