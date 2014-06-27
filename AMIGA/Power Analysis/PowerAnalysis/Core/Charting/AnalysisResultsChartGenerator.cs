using System.Linq;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AmigaPowerAnalysis.Core.Charting {
    public static class AnalysisResultsChartGenerator {

        private enum PlotType {
            Replicates,
            Ratio,
        }

        private static PlotModel CreatePlotModel(TestType testType, AnalysisMethodType analysisMethodType, PlotType plotType) {
            var plotModel = new PlotModel() {
                Title = testType.ToString() + " " + analysisMethodType.ToString(),
                TitleFontSize = 12,
                DefaultFontSize = 12,
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop,
                LegendBorder = OxyColors.Black,
            };

            var verticalAxis = new LinearAxis() {
                Title = "Power",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Minimum = 0,
                Maximum = 1,
            };
            plotModel.Axes.Add(verticalAxis);

            var lineAnnotation = new LineAnnotation() {
                Type = LineAnnotationType.Horizontal,
                Y = 0.8,
                Color = OxyColors.Red,
            };
            plotModel.Annotations.Add(lineAnnotation);

            return plotModel;
        }

        public static PlotModel CreatePlotViewReplicates(OutputPowerAnalysis outputPowerAnalysis, TestType testType, AnalysisMethodType analysisMethodType) {
            var model = CreatePlotModel(testType, analysisMethodType, PlotType.Replicates);

            var horizontalAxis = new LogarithmicAxis() {
                Title = "Replicates",
                Base = 2,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Bottom,
            };
            model.Axes.Add(horizontalAxis);

            if (outputPowerAnalysis != null) {
                var ratioGroups = outputPowerAnalysis.OutputRecords.GroupBy(r => r.Ratio);
                for (int i = 0; i < ratioGroups.Count(); ++i) {
                    var ratioGroup = ratioGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("Ratio = {0:0.##}", ratioGroup.Key);
                    if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.LogNormal) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceLogNormal,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.NegativeBinomial) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.OverdispersedPoisson) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.SquareRoot) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceSquareRoot,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.LogNormal) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceLogNormal,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.NegativeBinomial) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.OverdispersedPoisson) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.SquareRoot) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceSquareRoot,
                        }));
                    }
                    model.Series.Add(series);
                }
            }
            return model;
        }

        public static PlotModel CreatePlotViewLogRatio(OutputPowerAnalysis outputPowerAnalysis, TestType testType, AnalysisMethodType analysisMethodType) {
            var model = CreatePlotModel(testType, analysisMethodType, PlotType.Ratio);

            var horizontalAxis = new LinearAxis() {
                Title = "Log(ratio)",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Bottom,
            };
            model.Axes.Add(horizontalAxis);

            if (outputPowerAnalysis != null) {
                var replicateGroups = outputPowerAnalysis.OutputRecords.GroupBy(r => r.NumberOfReplicates);
                for (int i = 0; i < replicateGroups.Count(); ++i) {
                    var replicateGroup = replicateGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("Replicates = {0:0.##}", replicateGroup.Key);
                    if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.LogNormal) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerDifferenceLogNormal,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.NegativeBinomial) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerDifferenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.OverdispersedPoisson) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerDifferenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.SquareRoot) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerDifferenceSquareRoot,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.LogNormal) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerEquivalenceLogNormal,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.NegativeBinomial) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerEquivalenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.OverdispersedPoisson) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerEquivalenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.SquareRoot) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerEquivalenceSquareRoot,
                        }));
                    }
                    model.Series.Add(series);
                }
            }
            return model;
        }
    }
}
