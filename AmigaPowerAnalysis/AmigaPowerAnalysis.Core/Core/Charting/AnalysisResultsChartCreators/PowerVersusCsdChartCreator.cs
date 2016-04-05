using System.Collections.Generic;
using System.Linq;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators {

    public sealed class PowerVersusCsdChartCreator : AggregateAnalysisResultsChartCreatorBase {

        public List<int> BlockSizes { get; set; }

        public PowerVersusCsdChartCreator(List<AggregateOutputPowerAnalysisRecord> aggregatePowerAnalysisRecords, TestType testType, List<int> blockSizes)
            : base(aggregatePowerAnalysisRecords, testType) {
            BlockSizes = blockSizes;
        }

        public override PlotModel Create() {
            var plotModel = base.Create();
            return Create(AggregatePowerAnalysisRecords, TestType, BlockSizes);
        }

        public static PlotModel Create(IEnumerable<AggregateOutputPowerAnalysisRecord> aggregatePowerAnalysisRecords, TestType testType, List<int> blockSizes) {
            var model = AggregateAnalysisResultsChartCreatorBase.CreatePlotModel(testType);
            if (aggregatePowerAnalysisRecords != null && aggregatePowerAnalysisRecords.Count() > 0) {
                var horizontalAxis = new LinearAxis() {
                    Title = "LoC standardized difference",
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    Position = AxisPosition.Bottom,
                    AbsoluteMaximum = aggregatePowerAnalysisRecords.Max(r => r.ConcernStandardizedDifference),
                    AbsoluteMinimum = aggregatePowerAnalysisRecords.Min(r => r.ConcernStandardizedDifference),
                };
                model.Axes.Add(horizontalAxis);
                for (int i = 0; i < blockSizes.Count(); ++i) {
                    var replicateGroup = aggregatePowerAnalysisRecords
                        .Where(r => r.NumberOfReplications == blockSizes[i]);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("Repl {0}", blockSizes[i]);
                    series.Points.AddRange(replicateGroup.Select(g => new DataPoint() {
                        X = g.ConcernStandardizedDifference,
                        Y = g.GetPower(testType),
                    }));
                    model.Series.Add(series);
                }
            }
            return model;
        }
    }
}
