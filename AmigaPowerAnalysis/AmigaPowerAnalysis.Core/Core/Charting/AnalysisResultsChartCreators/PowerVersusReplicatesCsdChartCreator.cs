using AmigaPowerAnalysis.Core.PowerAnalysis;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Linq;
using System;

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
                var maxReplications = aggregatePowerAnalysisRecords.Max(r => r.NumberOfReplications);
                var minReplications = aggregatePowerAnalysisRecords.Min(r => r.NumberOfReplications);
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
                var csdGroups = aggregatePowerAnalysisRecords.GroupBy(r => r.ConcernStandardizedDifference).Where(g => !double.IsNaN(g.Key));
                for (int i = 0; i < csdGroups.Count(); ++i) {
                    var csdGroup = csdGroups.ElementAt(i);
                    var series = new LineSeries() {
                        MarkerType = (MarkerType)(i % 7 + 1),
                    };
                    series.Title = string.Format("CQ {0:0.##}", csdGroup.Key);
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
