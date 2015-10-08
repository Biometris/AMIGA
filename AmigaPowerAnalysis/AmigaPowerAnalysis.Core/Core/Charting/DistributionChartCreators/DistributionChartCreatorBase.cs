﻿using Biometris.Statistics.Distributions;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.WindowsForms;
using System.Collections.Generic;
using System.IO;

namespace AmigaPowerAnalysis.Core.Charting.DistributionChartCreators {

    public enum DistributionChartPreferenceType {
        Histogram,
        DistributionFunction,
        Both
    };

    public abstract class DistributionChartCreatorBase : IChartCreator {

        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public double Step { get; set; }
        public DistributionChartPreferenceType DistributionChartPreferenceType { get; set; }

        protected LinearAxis _horizontalAxis;

        public virtual PlotModel Create() {
            var plotModel = new PlotModel() {
                TitleFontSize = 11,
                DefaultFontSize = 11,
            };

            _horizontalAxis = new LinearAxis() {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
            };
            plotModel.Axes.Add(_horizontalAxis);

            var verticalAxis = new LinearAxis() {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                AbsoluteMinimum = 0,
                Minimum = 0,
            };
            plotModel.Axes.Add(verticalAxis);

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
