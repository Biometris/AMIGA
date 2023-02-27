using OpenHtmlToPdf;
using OxyPlot;
using OxyPlot.WindowsForms;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System;

namespace AmigaPowerAnalysis.Core.Reporting {

    public enum ChartCreationMethod {
        ExternalPng,
        InlinePng,
        InlineSvg
    }

    public abstract class ReportGeneratorBase {

        public abstract string Generate(ChartCreationMethod chartCreationMethod);

        public void SaveAsPdf(string fileName) {
            var html = Generate(ChartCreationMethod.ExternalPng);
            var pdf = Pdf
                .From(html)
                .OfSize(PaperSize.A4)
                .Content();
            File.WriteAllBytes(fileName, pdf);
        }

        public void SaveAsHtml(string fileName) {
            var html = Generate(ChartCreationMethod.ExternalPng);
            File.WriteAllText(fileName, html);
        }

        protected static string format(string htmlContent) {
            var assembly = Assembly.Load("AmigaPowerAnalysis");
            using (var textStreamReader = new StreamReader(assembly.GetManifestResourceStream("AmigaPowerAnalysis.Resources.print.css"))) {
                var style = textStreamReader.ReadToEnd();
                return string.Format("<html><head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=9\" /><style>{0}</style></head><body>{1}</body></html>", style, htmlContent);
            }
        }

        protected static void includeChart(PlotModel plotModel, int width, int height, string filePath, string imageFileName, StringBuilder stringBuilder, ChartCreationMethod chartCreationMethod) {
            switch (chartCreationMethod) {
                case ChartCreationMethod.ExternalPng:
                    includeChartAsPng(plotModel, width, height, filePath, imageFileName, stringBuilder);
                    break;
                case ChartCreationMethod.InlinePng:
                    includeChartAsInlinePng(plotModel, width, height, stringBuilder);
                    break;
                case ChartCreationMethod.InlineSvg:
                    includeChartAsSvg(plotModel, width, height, stringBuilder);
                    break;
                default:
                    break;
            }
        }

        protected static void includeChartAsPng(PlotModel plotModel, int width, int height, string filePath, string imageFileName, StringBuilder stringBuilder) {
            var imagesFolder = "Charts";
            string relativeImagePath = Path.Combine("Charts", imageFileName);
            string fullImagePath = Path.Combine(filePath, relativeImagePath);
            if (!Directory.Exists(Path.Combine(filePath, imagesFolder))) {
                Directory.CreateDirectory(Path.Combine(filePath, imagesFolder));
            }
            PngExporter.Export(plotModel, fullImagePath, width, height);
            //stringBuilder.Append("<img src=\"" + relativeImagePath + "\" />");
            stringBuilder.Append("<img src=\"" + Path.GetFullPath(fullImagePath) + "\" />");
        }

        protected static void includeChartAsSvg(PlotModel chart, int width, int height, StringBuilder stringBuilder) {
            var svgString = OxyPlot.SvgExporter.ExportToString(chart, width, height, false);
            var xmlDeclarationRegEx = new Regex(@"<\?xml.*?\?>\r\n");
            svgString = xmlDeclarationRegEx.Replace(svgString, string.Empty);
            var xmlDoctypeRegEx = new Regex(@"<\!DOCTYPE.*?\>\r\n");
            svgString = xmlDoctypeRegEx.Replace(svgString, string.Empty);
            stringBuilder.Append(svgString);
        }

        protected static void includeChartAsInlinePng(PlotModel chart, int width, int height, StringBuilder stringBuilder) {
            using (var stream = new MemoryStream()) {
                var scale = 4;
                OxyPlot.Wpf.PngExporter.Export(chart, stream, scale * width, scale * height, OxyColors.White, scale * 96);
                var imageBytes = stream.ToArray();
                var base64String = Convert.ToBase64String(imageBytes);
                stringBuilder.Append("<img width=\"" + width + "\" height=\"" + height + "\" src=\"" + string.Format("data:image/png;base64,{0}", base64String) + "\" />");
            }
        }
    }
}
