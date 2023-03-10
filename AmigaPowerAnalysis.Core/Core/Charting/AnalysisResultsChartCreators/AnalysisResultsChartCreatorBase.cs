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

    public abstract class AnalysisResultsChartCreatorBase : ChartCreatorBase {

        public TestType TestType { get; set; }
        public AnalysisMethodType AnalysisMethodType { get; set; }
        public List<OutputPowerAnalysisRecord> PowerAnalysisOutputRecords { get; set; }

        public AnalysisResultsChartCreatorBase(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType) {
            TestType = testType;
            AnalysisMethodType = analysisMethodType;
            PowerAnalysisOutputRecords = powerAnalysisOutputRecords;
        }

        public override PlotModel Create() {
            return CreatePlotModel(TestType, AnalysisMethodType);
        }

        protected static PlotModel CreatePlotModel(TestType testType, AnalysisMethodType analysisMethodType) {
            var plotModel = new PlotModel() {
                Title = testType.GetDisplayName() + " " + analysisMethodType.GetDisplayName(),
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
                MajorStep = 0.1,
                MinorStep = 0.05,
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
    }
}
