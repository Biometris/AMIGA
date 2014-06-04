using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AmigaPowerAnalysis.Core.Charting {
    public static class AnalysisResultsChartGenerator {

        private enum PlotType {
            Replicates,
            Ratio,
        }

        private static PlotModel CreatePlotModel(TestType testType, AnalysisType analysisType, PlotType plotType) {
            var plotModel = new PlotModel() {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop,
                LegendBorder = OxyColors.Black,
            };

            var verticalAxis = new LinearAxis();
            verticalAxis.MajorGridlineStyle = LineStyle.Solid;
            verticalAxis.MinorGridlineStyle = LineStyle.Dot;
            verticalAxis.Title = testType.ToString() + " " + analysisType.ToString();
            plotModel.Axes.Add(verticalAxis);

            var horizontalAxis = new LinearAxis();
            horizontalAxis.MajorGridlineStyle = LineStyle.Solid;
            horizontalAxis.MinorGridlineStyle = LineStyle.Dot;
            horizontalAxis.Position = AxisPosition.Bottom;
            horizontalAxis.Title = plotType.ToString();
            plotModel.Axes.Add(horizontalAxis);

            return plotModel;
        }

        public static PlotModel CreatePlotViewReplicates(OutputPowerAnalysis outputPowerAnalysis, TestType testType, AnalysisType analysisType) {
            var model = CreatePlotModel(testType, analysisType, PlotType.Replicates);
            if (outputPowerAnalysis != null) {
                var ratioGroups = outputPowerAnalysis.OutputRecords.GroupBy(r => r.Ratio);
                for (int i = 0; i < ratioGroups.Count(); ++i) {
                    var ratioGroup = ratioGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("Ratio = {0:0.##}", ratioGroup.Key);
                    if (testType == TestType.Difference && analysisType == AnalysisType.LogNormal) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceLogNormal,
                        }));
                    } else if (testType == TestType.Difference && analysisType == AnalysisType.NegativeBinomial) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Difference && analysisType == AnalysisType.OverdispersedPoisson) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Difference && analysisType == AnalysisType.SquareRoot) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerDifferenceSquareRoot,
                        }));
                    } else if (testType == TestType.Equivalence && analysisType == AnalysisType.LogNormal) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceLogNormal,
                        }));
                    } else if (testType == TestType.Equivalence && analysisType == AnalysisType.NegativeBinomial) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Equivalence && analysisType == AnalysisType.OverdispersedPoisson) {
                        series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                            X = g.NumberOfReplicates,
                            Y = g.PowerEquivalenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Equivalence && analysisType == AnalysisType.SquareRoot) {
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

        public static PlotModel CreatePlotViewLog(OutputPowerAnalysis outputPowerAnalysis, TestType testType, AnalysisType analysisType) {
            var model = CreatePlotModel(testType, analysisType, PlotType.Ratio);
            if (outputPowerAnalysis != null) {
                var replicateGroups = outputPowerAnalysis.OutputRecords.GroupBy(r => r.NumberOfReplicates);
                for (int i = 0; i < replicateGroups.Count(); ++i) {
                    var replicateGroup = replicateGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("Ratio = {0:0.##}", replicateGroup.Key);
                    if (testType == TestType.Difference && analysisType == AnalysisType.LogNormal) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerDifferenceLogNormal,
                        }));
                    } else if (testType == TestType.Difference && analysisType == AnalysisType.NegativeBinomial) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerDifferenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Difference && analysisType == AnalysisType.OverdispersedPoisson) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerDifferenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Difference && analysisType == AnalysisType.SquareRoot) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerDifferenceSquareRoot,
                        }));
                    } else if (testType == TestType.Equivalence && analysisType == AnalysisType.LogNormal) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerEquivalenceLogNormal,
                        }));
                    } else if (testType == TestType.Equivalence && analysisType == AnalysisType.NegativeBinomial) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerEquivalenceNegativeBinomial,
                        }));
                    } else if (testType == TestType.Equivalence && analysisType == AnalysisType.OverdispersedPoisson) {
                        series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                            X = g.LogRatio,
                            Y = g.PowerEquivalenceOverdispersedPoisson,
                        }));
                    } else if (testType == TestType.Equivalence && analysisType == AnalysisType.SquareRoot) {
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
