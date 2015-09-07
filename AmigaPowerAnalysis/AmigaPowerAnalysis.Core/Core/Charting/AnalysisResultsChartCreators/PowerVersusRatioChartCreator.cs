using System.Collections.Generic;
using System.Linq;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators {

    public sealed class PowerVersusRatioChartCreator : AnalysisResultsChartCreatorBase {

        public PowerVersusRatioChartCreator(TestType testType, AnalysisMethodType analysisMethodType)
            : base(testType, analysisMethodType) {
        }

        public override PlotModel Create() {
            var plotModel = base.Create();

            return plotModel;
        }

        public static PlotModel CreatePlotViewLogRatioReplicates(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType) {
            var model = AnalysisResultsChartCreatorBase.CreatePlotModel(testType, analysisMethodType);
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
                        Y = g.GetPower(testType, analysisMethodType),
                    }));
                    model.Series.Add(series);
                }
            }
            return model;
        }
    }
}
