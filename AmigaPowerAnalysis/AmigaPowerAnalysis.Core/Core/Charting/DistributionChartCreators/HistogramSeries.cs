using System;
using System.Collections.Generic;
using Biometris.Statistics.Histograms;
using OxyPlot;
using OxyPlot.Series;

namespace AmigaPowerAnalysis.Core.Charting.DistributionChartCreators {
    public sealed class HistogramSeries : XYAxisSeries {

        public HistogramSeries() {
            this.StrokeColor = OxyColors.Black;
            this.StrokeThickness = 1;
            this.FillColor = OxyColors.DarkGreen;
        }

        /// <summary>
        /// The default fill color.
        /// </summary>
        private OxyColor defaultFillColor;

        /// <summary>
        /// Gets or sets the color of the interior of the bars.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public OxyColor FillColor { get; set; }

        /// <summary>
        /// Gets the actual fill color.
        /// </summary>
        /// <value>The actual color.</value>
        public OxyColor ActualFillColor {
            get { return this.FillColor.IsUndefined() ? this.defaultFillColor : this.FillColor; }
        }

        /// <summary>
        /// Gets or sets the color of the border around the bars.
        /// </summary>
        /// <value>
        /// The color of the stroke.
        /// </value>
        public OxyColor StrokeColor { get; set; }

        /// <summary>
        /// Gets or sets the thickness of the curve.
        /// </summary>
        /// <value>The stroke thickness.</value>
        public double StrokeThickness { get; set; }

        /// <summary>
        /// Gets or sets the line style.
        /// </summary>
        /// <value>The line style.</value>
        public LineStyle LineStyle { get; set; }

        /// <summary>
        /// The histogram bins that are to be drawn.
        /// </summary>
        public IList<HistogramBin> Items { get; set; }
 
        public override void Render(IRenderContext rc, PlotModel model) {
            if (this.Items.Count == 0) {
                return;
            }

            this.VerifyAxes();

            var clippingRect = this.GetClippingRect();

            var fillColor = this.GetSelectableFillColor(this.FillColor);
            this.SetDefaultValues(model);

            foreach (var v in this.Items) {
                if (this.StrokeThickness > 0 && this.LineStyle != LineStyle.None) {
                    var leftBottom = this.Transform(v.XMinValue, 0);
                    var rightTop = this.Transform(v.XMaxValue, v.Frequency);
                    var rect = new OxyRect(leftBottom, rightTop);
                    rc.DrawClippedRectangleAsPolygon(clippingRect, rect, fillColor, this.StrokeColor, this.StrokeThickness);
                }
            }
        }

        /// <summary>
        /// Updates the maximum and minimum values of the series.
        /// </summary>
        protected override void UpdateMaxMin() {
            base.UpdateMaxMin();
            var xmin = double.MaxValue;
            var xmax = double.MinValue;
            var ymax = double.MinValue;
            foreach (var bar in this.Items) {
                xmin = Math.Min(xmin, bar.XMinValue);
                xmax = Math.Max(xmax, bar.XMaxValue);
                ymax = Math.Max(ymax, bar.Frequency);
            }
            this.MinX = Math.Max(this.XAxis.FilterMinValue, xmin);
            this.MaxX = Math.Min(this.XAxis.FilterMaxValue, xmax);
            this.MaxY = Math.Min(this.YAxis.FilterMaxValue, ymax);
        }
    }
}
