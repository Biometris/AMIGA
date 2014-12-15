using System.Collections.Generic;
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
                var ratioGroups = powerAnalysisOutputRecords.GroupBy(r => r.Effect).Where(g => !double.IsNaN(g.Key));
                for (int i = 0; i < ratioGroups.Count(); ++i) {
                    var ratioGroup = ratioGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("Ratio {0:0.##}", ratioGroup.Key);
                    series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                        X = g.NumberOfReplications,
                        Y = g.Power(testType, analysisMethodType),
                    }));
                    model.Series.Add(series);
                }
            }
            return model;
        }

        public static PlotModel CreatePlotViewLogRatioReplicates(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType) {
            var model = CreatePlotModel(testType, analysisMethodType, AnalysisPlotType.Ratio);
            var horizontalAxis = new LogarithmicAxis() {
                Title = "Ratio",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Bottom,
            };
            model.Axes.Add(horizontalAxis);
            if (powerAnalysisOutputRecords != null) {
                var replicateGroups = powerAnalysisOutputRecords.GroupBy(r => r.NumberOfReplications).Where(g => !double.IsNaN(g.Key));
                for (int i = 0; i < replicateGroups.Count(); ++i) {
                    var replicateGroup = replicateGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("Repl {0:0.##}", replicateGroup.Key);
                    series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                        X = g.Effect,
                        Y = g.Power(testType, analysisMethodType),
                    }));
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
                    series.Points.AddRange(csdGroup.Select(g => new DataPoint() {
                        X = g.NumberOfReplications,
                        Y = g.Power(testType, analysisMethodType),
                    }));
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
                var replicateGroups = powerAnalysisOutputRecords.GroupBy(r => r.NumberOfReplications).Where(g => !double.IsNaN(g.Key));
                for (int i = 0; i < replicateGroups.Count(); ++i) {
                    var replicateGroup = replicateGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("Repl {0:0.##}", replicateGroup.Key);
                    series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                        X = g.ConcernStandardizedDifference,
                        Y = g.Power(testType, analysisMethodType),
                    }));
                    model.Series.Add(series);
                }
            }
            return model;
        }
    }
}
