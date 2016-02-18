using System.IO;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace AmigaPowerAnalysis.Core.Charting {

    public abstract class ChartCreatorBase {

        public abstract PlotModel Create();

        public void SaveToFile(string filename, int width = 600, int height = 300) {
            var plot = Create();
            if (string.IsNullOrEmpty(Path.GetExtension(filename))) {
                filename += ".png";
            }
            PngExporter.Export(plot, filename, width, height);
        }
    }
}
