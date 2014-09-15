using System.Collections.Generic;
using System.Linq;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using AmigaPowerAnalysis.Helpers.ClassExtensionMethods;
using AmigaPowerAnalysis.Core.PowerAnalysis;

namespace AmigaPowerAnalysis.Core.Charting {
    public static class AnalysisResultsChartGenerator {

        private enum PlotType {
            Replicates,
            Ratio,
            LevelOfConcern,
        }

        private static PlotModel CreatePlotModel(TestType testType, AnalysisMethodType analysisMethodType, PlotType plotType) {
            var plotModel = new PlotModel() {
                Title = testType.GetDisplayName() + " " + analysisMethodType.GetDisplayName(),
                TitleFontSize = 11,
                DefaultFontSize = 11,
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
                Color = OxyColors.Black,
            };
            plotModel.Annotations.Add(lineAnnotation);

            return plotModel;
        }

        public static PlotModel CreatePlotViewReplicatesLogRatio(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType) {
            var model = CreatePlotModel(testType, analysisMethodType, PlotType.Replicates);

            var horizontalAxis = new LogarithmicAxis() {
                Title = "Replicates",
                Base = 2,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Bottom,
            };
            model.Axes.Add(horizontalAxis);

            if (powerAnalysisOutputRecords != null) {
                var ratioGroups = powerAnalysisOutputRecords.GroupBy(r => r.Ratio).Where(g => !double.IsNaN(g.Key));
                for (int i = 0; i < ratioGroups.Count(); ++i) {
                    var ratioGroup = ratioGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("Ratio {0:0.##}", ratioGroup.Key);
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

        public static PlotModel CreatePlotViewLogRatioReplicates(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType) {
            var model = CreatePlotModel(testType, analysisMethodType, PlotType.Ratio);

            var horizontalAxis = new LinearAxis() {
                Title = "Log(ratio)",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Bottom,
            };
            model.Axes.Add(horizontalAxis);

            if (powerAnalysisOutputRecords != null) {
                var replicateGroups = powerAnalysisOutputRecords.GroupBy(r => r.NumberOfReplicates).Where(g => !double.IsNaN(g.Key));
                for (int i = 0; i < replicateGroups.Count(); ++i) {
                    var replicateGroup = replicateGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("Repl {0:0.##}", replicateGroup.Key);
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

        public static PlotModel CreatePlotViewReplicatesLevelOfConcern(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType) {
            var model = CreatePlotModel(testType, analysisMethodType, PlotType.Replicates);

            var horizontalAxis = new LogarithmicAxis() {
                Title = "Replicates",
                Base = 2,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Bottom,
            };
            model.Axes.Add(horizontalAxis);

            if (powerAnalysisOutputRecords != null) {
                var levelOfConcernGroups = powerAnalysisOutputRecords.GroupBy(r => r.LevelOfConcern).Where(g => !double.IsNaN(g.Key));
                for (int i = 0; i < levelOfConcernGroups.Count(); ++i) {
                    var levelOfConcernGroup = levelOfConcernGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("CSD {0:0.##}", levelOfConcernGroup.Key);
                    if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.LogNormal) {
                        series.Points.AddRange(levelOfConcernGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceLogNormal,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.NegativeBinomial) {
                        series.Points.AddRange(levelOfConcernGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.OverdispersedPoisson) {
                        series.Points.AddRange(levelOfConcernGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.SquareRoot) {
                        series.Points.AddRange(levelOfConcernGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceSquareRoot,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.LogNormal) {
                        series.Points.AddRange(levelOfConcernGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceLogNormal,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.NegativeBinomial) {
                        series.Points.AddRange(levelOfConcernGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.OverdispersedPoisson) {
                        series.Points.AddRange(levelOfConcernGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.SquareRoot) {
                        series.Points.AddRange(levelOfConcernGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceSquareRoot,
                        }));
                    }
                    model.Series.Add(series);
                }
            }
            return model;
        }

        public static PlotModel CreatePlotViewLevelOfConcernReplicates(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType) {
            var model = CreatePlotModel(testType, analysisMethodType, PlotType.LevelOfConcern);

            var horizontalAxis = new LinearAxis() {
                Title = "Concern Standardized Difference",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Bottom,
            };
            model.Axes.Add(horizontalAxis);

            if (powerAnalysisOutputRecords != null) {
                var replicateGroups = powerAnalysisOutputRecords.GroupBy(r => r.NumberOfReplicates).Where(g => !double.IsNaN(g.Key));
                for (int i = 0; i < replicateGroups.Count(); ++i) {
                    var replicateGroup = replicateGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("Repl {0:0.##}", replicateGroup.Key);
                    if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.LogNormal) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LevelOfConcern,
                            Y = g.PowerDifferenceLogNormal,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.NegativeBinomial) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LevelOfConcern,
                            Y = g.PowerDifferenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.OverdispersedPoisson) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LevelOfConcern,
                            Y = g.PowerDifferenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.SquareRoot) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LevelOfConcern,
                            Y = g.PowerDifferenceSquareRoot,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.LogNormal) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LevelOfConcern,
                            Y = g.PowerEquivalenceLogNormal,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.NegativeBinomial) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LevelOfConcern,
                            Y = g.PowerEquivalenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.OverdispersedPoisson) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LevelOfConcern,
                            Y = g.PowerEquivalenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.SquareRoot) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LevelOfConcern,
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
