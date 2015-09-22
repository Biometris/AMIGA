using System.Collections.Generic;
using System.Linq;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators {

    public sealed class PowerVersusCsdChartCreator : AnalysisResultsChartCreatorBase {

        public List<int> BlockSizes { get; set; }

        public PowerVersusCsdChartCreator(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType, List<int> blockSizes)
            : base(powerAnalysisOutputRecords, testType, analysisMethodType) {
            BlockSizes = blockSizes;
        }

        public override PlotModel Create() {
            var plotModel = base.Create();
            return Create(PowerAnalysisOutputRecords, TestType, AnalysisMethodType, BlockSizes);
        }

        public static PlotModel Create(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType, List<int> blockSizes) {
            var model = AnalysisResultsChartCreatorBase.CreatePlotModel(testType, analysisMethodType);
            var horizontalAxis = new LinearAxis() {
                Title = "Concern Standardized Difference",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Bottom,
                AbsoluteMaximum = powerAnalysisOutputRecords.Max(r => r.ConcernStandardizedDifference),
                AbsoluteMinimum = powerAnalysisOutputRecords.Min(r => r.ConcernStandardizedDifference),
            };
            model.Axes.Add(horizontalAxis);
            if (powerAnalysisOutputRecords != null) {
                for (int i = 0; i < blockSizes.Count(); ++i) {
                    var replicateGroup = powerAnalysisOutputRecords
                        .Where(r => r.NumberOfReplications == blockSizes[i]);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("Repl {0}", blockSizes[i]);
                    series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                        X = g.ConcernStandardizedDifference,
                        Y = g.GetPower(testType, analysisMethodType),
                    }));
                    model.Series.Add(series);
                }
            }
            return model;
        }
    }
}
