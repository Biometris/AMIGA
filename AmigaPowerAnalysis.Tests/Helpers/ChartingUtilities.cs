using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Charting;
using AmigaPowerAnalysis.Core.DataAnalysis;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Core.Reporting;
using Biometris.ExtensionMethods;
using Biometris.ProgressReporting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AmigaPowerAnalysis.Tests.TestUtilities {

    [TestClass]
    public static class ChartingUtilities {

        private static string _testPath = Path.Combine(Properties.Settings.Default.TestPath, "ChartCreation");

        public static string SaveChart<T>(this T testClass, IChartCreator chartCreator, string filename, int width=600, int height=300) {
            var outputPath = Path.Combine(_testPath, typeof(T).Name);
            if (!Directory.Exists(outputPath)) {
                Directory.CreateDirectory(outputPath);
            }
            var fullFilePath = Path.Combine(outputPath, filename);
            chartCreator.SaveToFile(fullFilePath, width, height);
            return Path.GetFullPath(fullFilePath);
        }
    }
}
