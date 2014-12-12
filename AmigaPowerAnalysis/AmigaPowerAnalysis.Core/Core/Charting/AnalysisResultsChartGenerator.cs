﻿using System.Collections.Generic;
using System.Linq;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Helpers.ClassExtensionMethods;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AmigaPowerAnalysis.Core.Charting {

    public enum AnalysisPlotType {
        Replicates,
        Ratio,
        ConcernStandardizedDifference,
    }

    public static class AnalysisResultsChartGenerator {

        private static PlotModel CreatePlotModel(TestType testType, AnalysisMethodType analysisMethodType, AnalysisPlotType plotType) {
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
                Minimum = -0.01,
                Maximum = 1.1,
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
            var model = CreatePlotModel(testType, analysisMethodType, AnalysisPlotType.Replicates);

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
            var model = CreatePlotModel(testType, analysisMethodType, AnalysisPlotType.Ratio);

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

        public static PlotModel CreatePlotViewReplicatesConcernStandardizedDifference(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType) {
            var model = CreatePlotModel(testType, analysisMethodType, AnalysisPlotType.Replicates);

            var horizontalAxis = new LogarithmicAxis() {
                Title = "Replicates",
                Base = 2,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Bottom,
            };
            model.Axes.Add(horizontalAxis);

            if (powerAnalysisOutputRecords != null) {
                var csdGroups = powerAnalysisOutputRecords.GroupBy(r => r.ConcernStandardizedDifference).Where(g => !double.IsNaN(g.Key));
                for (int i = 0; i < csdGroups.Count(); ++i) {
                    var csdGroup = csdGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("CSD {0:0.##}", csdGroup.Key);
                    if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.LogNormal) {
                        series.Points.AddRange(csdGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceLogNormal,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.NegativeBinomial) {
                        series.Points.AddRange(csdGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.OverdispersedPoisson) {
                        series.Points.AddRange(csdGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.SquareRoot) {
                        series.Points.AddRange(csdGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceSquareRoot,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.LogNormal) {
                        series.Points.AddRange(csdGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceLogNormal,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.NegativeBinomial) {
                        series.Points.AddRange(csdGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.OverdispersedPoisson) {
                        series.Points.AddRange(csdGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.SquareRoot) {
                        series.Points.AddRange(csdGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceSquareRoot,
                        }));
                    }
                    model.Series.Add(series);
                }
            }
            return model;
        }

        public static PlotModel CreatePlotViewConcernStandardizedDifferenceReplicates(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType) {
            var model = CreatePlotModel(testType, analysisMethodType, AnalysisPlotType.ConcernStandardizedDifference);

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
                            X = g.ConcernStandardizedDifference,
                            Y = g.PowerDifferenceLogNormal,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.NegativeBinomial) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.ConcernStandardizedDifference,
                            Y = g.PowerDifferenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.OverdispersedPoisson) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.ConcernStandardizedDifference,
                            Y = g.PowerDifferenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Difference && analysisMethodType == AnalysisMethodType.SquareRoot) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.ConcernStandardizedDifference,
                            Y = g.PowerDifferenceSquareRoot,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.LogNormal) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.ConcernStandardizedDifference,
                            Y = g.PowerEquivalenceLogNormal,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.NegativeBinomial) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.ConcernStandardizedDifference,
                            Y = g.PowerEquivalenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.OverdispersedPoisson) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.ConcernStandardizedDifference,
                            Y = g.PowerEquivalenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Equivalence && analysisMethodType == AnalysisMethodType.SquareRoot) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.ConcernStandardizedDifference,
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
