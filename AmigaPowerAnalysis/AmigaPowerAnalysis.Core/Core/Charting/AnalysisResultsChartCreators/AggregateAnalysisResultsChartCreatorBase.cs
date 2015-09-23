using System.Collections.Generic;
using System.IO;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.ExtensionMethods;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;

namespace AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators {

    public enum AnalysisPlotType {
        Replicates,
        Ratio,
        ConcernStandardizedDifference,
    }

    public abstract class AggregateAnalysisResultsChartCreatorBase : IChartCreator {

        public TestType TestType { get; set; }
        public List<AggregateOutputPowerAnalysisRecord> AggregatePowerAnalysisRecords { get; set; }

        public AggregateAnalysisResultsChartCreatorBase(List<AggregateOutputPowerAnalysisRecord> aggregatePowerAnalysisRecords, TestType testType) {
            TestType = testType;
            AggregatePowerAnalysisRecords = aggregatePowerAnalysisRecords;
        }

        public virtual PlotModel Create() {
            return CreatePlotModel(TestType);
        }

        protected static PlotModel CreatePlotModel(TestType testType) {
            var plotModel = new PlotModel() {
                Title = testType.GetDisplayName(),
                TitleFontSize = 11,
                DefaultFontSize = 11,
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop,
                //LegendBorder = OxyColors.Black,
                LegendBorderThickness = 0,
                //LegendMargin = 4,
                LegendPadding = 10,
                LegendItemSpacing = 3,
            };
            var verticalAxis = new LinearAxis() {
                Title = "Power",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                AbsoluteMinimum = -0.01,
                AbsoluteMaximum = 1.1,
                Minimum = -0.01,
                Maximum = 1.01,
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

        public void SaveToFile(string filename, int width = 600, int height = 300) {
            var plot = Create();
            if(string.IsNullOrEmpty(Path.GetExtension(filename))) {
                filename += ".png";
            }
            PngExporter.Export(plot, filename, width, height);
        }
    }
}
