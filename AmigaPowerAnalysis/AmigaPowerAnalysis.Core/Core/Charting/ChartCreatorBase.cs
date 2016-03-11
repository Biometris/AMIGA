using System.IO;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace AmigaPowerAnalysis.Core.Charting {

    public abstract class ChartCreatorBase : IChartCreator {

        public abstract PlotModel Create();

        public void SaveToFile(string filename, int width, int height) {
            var plot = Create();
            if (string.IsNullOrEmpty(Path.GetExtension(filename))) {
                filename += ".png";
            }
            PngExporter.Export(plot, filename, width, height);
        }

        public void SaveToFile(string filename) {
            SaveToFile(filename, 600, 350);
        }
    }
}
