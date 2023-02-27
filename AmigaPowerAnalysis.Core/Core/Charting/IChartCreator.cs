using OxyPlot;

namespace AmigaPowerAnalysis.Core.Charting {

    public interface IChartCreator {
        PlotModel Create();
        void SaveToFile(string filename);
        void SaveToFile(string filename, int width, int height);
    }
}
