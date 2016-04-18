using System;
using System.Collections.Generic;
using System.Linq;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators {

    public sealed class PowerVersusReplicatesRatioChartCreator : AnalysisResultsChartCreatorBase {

        public PowerVersusReplicatesRatioChartCreator(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType)
            : base(powerAnalysisOutputRecords, testType, analysisMethodType) {
        }

        public override PlotModel Create() {
            var plotModel = base.Create();
            return Create(PowerAnalysisOutputRecords, TestType, AnalysisMethodType);
        }

        public static PlotModel Create(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType) {
            var model = AnalysisResultsChartCreatorBase.CreatePlotModel(testType, analysisMethodType);
            if (powerAnalysisOutputRecords != null && powerAnalysisOutputRecords.Count > 0) {
                var maxReplications = powerAnalysisOutputRecords.Max(r => r.NumberOfReplications);
                var minReplications = powerAnalysisOutputRecords.Min(r => r.NumberOfReplications);
                var horizontalAxis = new LogarithmicAxis() {
                    Title = "Replicates",
                    Base = 2,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    Position = AxisPosition.Bottom,
                    AbsoluteMinimum = (minReplications != maxReplications) ? minReplications : Math.Ceiling(minReplications / 2d),
                    AbsoluteMaximum = (minReplications != maxReplications) ? maxReplications : Math.Ceiling(maxReplications * 2d),
                };
                model.Axes.Add(horizontalAxis);
                var ratioGroups = powerAnalysisOutputRecords.GroupBy(r => r.Effect).Where(g => !double.IsNaN(g.Key));
                for (int i = 0; i < ratioGroups.Count(); ++i) {
                    var ratioGroup = ratioGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("Ratio {0:0.##}", ratioGroup.Key);
                    series.Points.AddRange(ratioGroup.Select(g => new DataPoint() {
                        X = g.NumberOfReplications,
                        Y = g.GetPower(testType, analysisMethodType),
                    }));
                    model.Series.Add(series);
                }
            }
            return model;
        }
    }
}
