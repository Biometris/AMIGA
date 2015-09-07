﻿using System.Collections.Generic;
using System.Linq;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators {

    public sealed class PowerVersusReplicatesRatioChartCreator : AnalysisResultsChartCreatorBase {

        public PowerVersusReplicatesRatioChartCreator(TestType testType, AnalysisMethodType analysisMethodType)
            : base(testType, analysisMethodType) {
        }

        public override PlotModel Create() {
            var plotModel = base.Create();

            return plotModel;
        }

        public static PlotModel CreatePlotViewReplicatesLogRatio(List<OutputPowerAnalysisRecord> powerAnalysisOutputRecords, TestType testType, AnalysisMethodType analysisMethodType) {
            var model = AnalysisResultsChartCreatorBase.CreatePlotModel(testType, analysisMethodType);
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
                        Y = g.GetPower(testType, analysisMethodType),
                    }));
                    model.Series.Add(series);
                }
            }
            return model;
        }
    }
}
