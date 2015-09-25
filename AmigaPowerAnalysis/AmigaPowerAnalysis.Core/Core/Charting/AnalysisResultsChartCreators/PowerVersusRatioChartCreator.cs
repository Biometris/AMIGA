using System.Collections.Generic;
using System.Linq;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.Statistics.Measurements;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators {

    public sealed class PowerVersusRatioChartCreator : AnalysisResultsChartCreatorBase {

        public List<int> BlockSizes { get; set; }
        public MeasurementType MeasurementType { get; set; }

        public PowerVersusRatioChartCreator(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType, MeasurementType measurementType, List<int> blockSizes)
            : base(powerAnalysisOutputRecords, testType, analysisMethodType) {
            BlockSizes = blockSizes;
            MeasurementType = measurementType;
        }

        public override PlotModel Create() {
            var plotModel = base.Create();
            return Create(PowerAnalysisOutputRecords, TestType, AnalysisMethodType, MeasurementType, BlockSizes);
        }

        public static PlotModel Create(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType, MeasurementType measurementType, List<int> blockSizes) {
            var model = AnalysisResultsChartCreatorBase.CreatePlotModel(testType, analysisMethodType);
            Axis horizontalAxis = null;
            if (measurementType == MeasurementType.Continuous) {
                horizontalAxis = new LinearAxis() {
                    Title = "Diff",
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    Position = AxisPosition.Bottom,
                    AbsoluteMaximum = powerAnalysisOutputRecords.Max(r => r.Effect),
                    AbsoluteMinimum = powerAnalysisOutputRecords.Min(r => r.Effect),
                };
            } else {
                horizontalAxis = new LogarithmicAxis() {
                    Title = "Ratio",
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot,
                    Position = AxisPosition.Bottom,
                    AbsoluteMaximum = powerAnalysisOutputRecords.Max(r => r.Effect),
                    AbsoluteMinimum = powerAnalysisOutputRecords.Min(r => r.Effect),
                };
            }
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
