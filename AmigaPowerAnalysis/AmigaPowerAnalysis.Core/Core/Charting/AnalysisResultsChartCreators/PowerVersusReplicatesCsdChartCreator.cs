using AmigaPowerAnalysis.Core.PowerAnalysis;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Linq;

namespace AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators {

    public sealed class PowerVersusReplicatesCsdChartCreator : AggregateAnalysisResultsChartCreatorBase {

        public PowerVersusReplicatesCsdChartCreator(List<AggregateOutputPowerAnalysisRecord> aggregatePowerAnalysisRecords, TestType testType)
            : base(aggregatePowerAnalysisRecords, testType) {
        }

        public override PlotModel Create() {
            var plotModel = base.Create();
            return Create(AggregatePowerAnalysisRecords, TestType);
        }

        public static PlotModel Create(IEnumerable<AggregateOutputPowerAnalysisRecord> aggregatePowerAnalysisRecords, TestType testType) {
            var model = AggregateAnalysisResultsChartCreatorBase.CreatePlotModel(testType);
            if (aggregatePowerAnalysisRecords != null && aggregatePowerAnalysisRecords.Count() > 0) {
                var horizontalAxis = new LogarithmicAxis() {
                    Title = "Replicates",
                    Base = 2,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    Position = AxisPosition.Bottom,
                    AbsoluteMaximum = aggregatePowerAnalysisRecords.Max(r => r.NumberOfReplications),
                    AbsoluteMinimum = aggregatePowerAnalysisRecords.Min(r => r.NumberOfReplications),
                };
                model.Axes.Add(horizontalAxis);
                var csdGroups = aggregatePowerAnalysisRecords.GroupBy(r => r.ConcernStandardizedDifference).Where(g => !double.IsNaN(g.Key));
                for (int i = 0; i < csdGroups.Count(); ++i) {
                    var csdGroup = csdGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("CSD {0:0.##}", csdGroup.Key);
                    series.Points.AddRange(csdGroup.Select(g => new DataPoint() {
                        X = g.NumberOfReplications,
                        Y = g.GetPower(testType),
                    }));
                    model.Series.Add(series);
                }
            }
            return model;
        }
    }
}
