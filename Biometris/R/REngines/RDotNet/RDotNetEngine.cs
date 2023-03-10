using RDotNet;
using RDotNet.Devices;
using RDotNet.NativeLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Biometris.R.REngines {
    public class RDotNetEngine : IRCommandExecuter, IDisposable {

        /// <summary>
        /// Path to the R Home folder, e.g. C:\Program Files\R\R-3.6.0
        /// </summary>
        public static string R_HomePath { get; set; } = null;

        /// <summary>
        /// Path to the R packages library, e.g. C:\Program Files\R\R-3.6.0\library
        /// If no value is given it defaults to the [R_HomePath]\library folder
        /// </summary>
        public static string R_LibsPath { get; set; } = null;

        /// <summary>
        /// Is the R script already running (doing some analysis).
        /// You have to wait till it is free again, otherwise commands may interfere.
        /// </summary>
        private static bool _isRunning = false;

        /// <summary>
        /// The R.Net engine.
        /// </summary>
        private static REngine _rEngine = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public RDotNetEngine() {
            start();
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~RDotNetEngine() {
            Dispose(false);
        }

        #region IDisposable

        /// <summary>
        /// Dispose implementation for IDisposable.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing) {
            stop();
        }

        #endregion

        #region Start/Stop/IsRunning

        /// <summary>
        /// Is the R script already running (doing some analysis).
        /// You have to wait till it is free again, otherwise commands may interfere.
        /// </summary>
        public bool IsRunning {
            get { return _isRunning; }
        }

        /// <summary>
        /// Tell that you want to process some R-commands.
        /// This will block other threads to use R (if used properply)
        /// </summary>
        private void start() {
            if (_isRunning) {
                throw new Exception("Another instance of the R.Net engine is already running");
            }
            _isRunning = true;
            if (_rEngine == null) {
                string rPath = "R path not found";
                try {
                    var nativeUtils = new NativeUtility();
                    var rHome = !string.IsNullOrEmpty(R_HomePath) ? R_HomePath : nativeUtils.FindRHome();
                    rPath = nativeUtils.FindRPath(rHome);
                    if (!string.IsNullOrEmpty(R_LibsPath)) {
                        Environment.SetEnvironmentVariable("R_LIBS_USER", R_LibsPath, EnvironmentVariableTarget.Process);
                    }
                    REngine.SetEnvironmentVariables(rPath, rHome);
                    _rEngine = REngine.GetInstance(device: new NullCharacterDevice());
                    if (_rEngine.DllVersion.StartsWith("4.2")) {
                        var cmd = $"Sys.setenv(PATH = paste(\"{rPath.Replace(@"\", "/")}\", Sys.getenv(\"PATH\"), sep=\";\"))";
                        _rEngine.Evaluate(cmd);
                    }
                } catch {
                    _isRunning = false;
                    throw new Exception($"Cannot establish connection with R ({rPath}).");
                }
                // Initialize R session
                if (_rEngine != null) {
                    _rEngine.Evaluate("options(width=10000)");
                }
            }
            evaluateCommand("rm(list = ls())");
        }

        /// <summary>
        /// Tell that you're ready with using R-commands. Always use this!
        /// </summary>
        private void stop() {
            if (_rEngine != null) {
                try {
                    var cmd = "rm(list = ls())";
                    _rEngine.Evaluate(cmd);
                } catch (Exception ex) {
                    throw new REvaluateException(ex.Message);
                }
            }
            _isRunning = false;
        }

        #endregion

        #region Evaluation interface

        /// <summary>
        /// Evaluates the given R command in the R environment.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private SymbolicExpression evaluateCommand(string command) {
            try {
                LogCommand(command);
                return _rEngine.Evaluate(command);
            } catch (ParseException ex) {
                var sb = new StringBuilder();
                sb.AppendLine("Error while executing R script.");
                sb.Append(string.Format("Exception: {0}", ex.Message));
                throw new Exception(sb.ToString());
            } catch (Exception) {
                var sb = new StringBuilder();
                sb.AppendLine("Error while executing R script.");
                sb.Append(string.Format("Exception: {0}", GetErrorMessage()));
                throw new Exception(sb.ToString());
            }
        }

        #endregion

        #region Assignments and evaluations

        /// <summary>
        /// Evaluates the given R command in the R environment.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public void EvaluateNoReturn(string command) {
            evaluateCommand(command);
        }

        /// <summary>
        /// Evaluates the R variable as a boolean.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void Comment(string message) {
            EvaluateNoReturn(string.Format("#{0}", message));
        }

        /// <summary>
        /// Assigns an integer in the R environment.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="expression"></param>
        public void SetSymbol(string name, int value) {
            var cmd = string.Format("{0} <- {1}", name, value);
            EvaluateNoReturn(cmd);
        }

        /// <summary>
        /// Assigns a double variable in the R environment.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="expression"></param>
        public void SetSymbol(string name, double value) {
            var cmd = string.Format("{0} <- {1}", name, value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            EvaluateNoReturn(cmd);
        }

        /// <summary>
        /// Assigns an integer vector variable in the R environment.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="expression"></param>
        public void SetSymbol(string name, IEnumerable<int> values) {
            var cmd = string.Format("{0} <- c({1})", name, string.Join(", ", values.Select(v => v.ToString(System.Globalization.CultureInfo.InvariantCulture))));
            EvaluateNoReturn(cmd);
        }

        /// <summary>
        /// Assigns a multi-dimensional array of integers in the R environment.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="expression"></param>
        public void SetSymbol(string name, int[,] values) {
            var rows = values.GetLength(0);
            var columns = values.GetLength(1);
            var flatValues = string.Join(", ", values.Cast<int>().Select(v => v.ToString(System.Globalization.CultureInfo.InvariantCulture)));
            var cmd = string.Format("{0} <- matrix(c({1}), nrow = {2}, ncol = {3}, TRUE)", name, flatValues, rows, columns);
            EvaluateNoReturn(cmd);
        }

        /// <summary>
        /// Assigns a numeric vector of doubles in the R environment.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="expression"></param>
        public void SetSymbol(string name, IEnumerable<double> values) {
            var cmd = string.Format("{0} <- c({1})", name, string.Join(", ", values.Select(v => v.ToString(System.Globalization.CultureInfo.InvariantCulture))));
            EvaluateNoReturn(cmd);
        }

        /// <summary>
        /// Assigns a multi-dimensional array of doubles in the R environment.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="expression"></param>
        public void SetSymbol(string name, double[,] values) {
            var rows = values.GetLength(0);
            var columns = values.GetLength(1);
            var flatValues = string.Join(", ", values.Cast<double>().Select(v => v.ToString(System.Globalization.CultureInfo.InvariantCulture)));
            var cmd = string.Format("{0} <- matrix(c({1}), nrow = {2}, ncol = {3}, TRUE)", name, flatValues, rows, columns);
            EvaluateNoReturn(cmd);
        }

        /// <summary>
        /// Assigns a string variable in the R environment.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="expression"></param>
        public void SetSymbol(string name, List<string> values) {
            var cmd = string.Format("{0} <- c({1})", name, string.Join(", ", values.Select(v => "'" + v + "'")));
            EvaluateNoReturn(cmd);
        }

        /// <summary>
        /// Assigns a data table as a data frame in the R environment.
        /// </summary>
        /// <param name="table"></param>
        public void SetSymbol(string name, DataTable table) {
            var rows = table.Rows.Cast<DataRow>().ToList();
            var columns = table.Columns.Cast<DataColumn>().ToList();
            for (int i = 0; i < table.Columns.Count; ++i) {
                var column = table.Columns[i];
                if (column.DataType == typeof(double)) {
                    var values = rows.Select(r => (double)r[i]).ToList();
                    SetSymbol(column.ColumnName, values);
                } else if (column.DataType == typeof(int)) {
                    var values = rows.Select(r => (int)r[i]).ToList();
                    SetSymbol(column.ColumnName, values);
                } else {
                    var values = rows.Select(r => r[i].ToString()).ToList();
                    SetSymbol(column.ColumnName, values);
                    EvaluateNoReturn(string.Format("{0} <- factor({0})", column.ColumnName));
                }
            }
            EvaluateNoReturn(string.Format("{0} = data.frame({1})", name, string.Join(",", columns.Select(c => c.ColumnName))));
        }

        /// <summary>
        /// Evaluates the R variable as a boolean.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool EvaluateBoolean(string name) {
            return evaluateCommand(name).AsLogical().First();
        }

        /// <summary>
        /// Evaluates the R variable as an integer.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int EvaluateInteger(string name) {
            return evaluateCommand(name).AsInteger().First();
        }

        /// <summary>
        /// Evaluates the R variable as a double.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public double EvaluateDouble(string name) {
            return evaluateCommand(name).AsNumeric().First();
        }

        /// <summary>
        /// Evaluates the variable with the specified name as a string.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string EvaluateString(string name) {
            var values = evaluateCommand(name).AsCharacter();
            return string.Join("\n", values);
        }

        /// <summary>
        /// Evaluates the R variable as a list of integers.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<int> EvaluateIntegerVector(string name) {
            var values = evaluateCommand(name).AsInteger();
            return values.ToList();
        }

        /// <summary>
        /// Evaluates the R variable as a two-dimensional array (i.e., a matrix)
        /// of integers.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int[,] EvaluateIntegerMatrix(string name) {
            var values = evaluateCommand(name).AsIntegerMatrix();
            return values.ToArray();
        }

        /// <summary>
        /// Evaluates the R variable as a list of doubles.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<double> EvaluateNumericVector(string name) {
            var values = evaluateCommand(name).AsNumeric();
            return values.ToList();
        }

        /// <summary>
        /// Evaluates the R variable as a two-dimensional array (i.e., a matrix)
        /// of integers.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public double[,] EvaluateMatrix(string name) {
            var values = evaluateCommand(name).AsNumericMatrix();
            return values.ToArray();
        }

        /// <summary>
        /// Evaluates the R variable as a list of strings.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<string> EvaluateCharacterVector(string name) {
            var values = evaluateCommand(name).AsCharacter();
            return values.ToList();
        }

        /// <summary>
        /// Captures the output of the specified command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string CaptureOutput(string command) {
            var values = evaluateCommand(string.Format("capture.output({0})", command)).AsCharacter();
            return string.Join("\n", values);
        }

        /// <summary>
        /// Evaluates an R data frame as a datatable.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable EvaluateDataTable(string name) {
            var dataset = evaluateCommand(name).AsDataFrame();
            var table = new DataTable();
            for (int i = 0; i < dataset.ColumnCount; ++i) {
                table.Columns.Add(dataset.ColumnNames[i], typeof(string));
            }
            for (int i = 0; i < dataset.RowCount; ++i) {
                var row = table.Rows.Add();
                for (int k = 0; k < dataset.ColumnCount; ++k) {
                    row[k] = dataset[i, k].ToString();
                }
            }
            return table;
        }

        /// <summary>
        /// Returns the latest error message of R.
        /// </summary>
        /// <returns></returns>
        public string GetErrorMessage() {
            return evaluateCommand("geterrmessage()").AsCharacter().First();
        }

        /// <summary>
        /// Returns the dll version of R that is used by this engine.
        /// </summary>
        /// <returns></returns>
        public string GetRVerion() {
            return _rEngine.DllVersion;
        }

        /// <summary>
        /// Returns information about the used R version in a printable string.
        /// </summary>
        /// <returns></returns>
        public string GetRInfo() {
            var sb = new StringBuilder();
            var version = EvaluateString("R.version.string");
            sb.AppendLine(version);
            var rHome = EvaluateString("R.home(\"bin\")");
            sb.AppendLine(string.Format("R home: {0}", rHome));
            var libPaths = EvaluateCharacterVector(".libPaths()");
            sb.AppendLine(string.Format("Library paths: {0}", string.Join(", ", libPaths)));
            return sb.ToString();
        }

        /// <summary>
        /// Tries to load the R package with the specified package name. If the package cannot be found
        /// locally, this method attempts to download and install it from the cran repository.
        /// </summary>
        /// <param name="packageName">The R package name.</param>
        /// <param name="minimalRequiredPackageVersion">The minimally required version.</param>
        public void LoadLibrary(string packageName, Version minimalRequiredPackageVersion = null) {
            var libLoaded = EvaluateBoolean($"require('{packageName}')");
            if (!libLoaded) {
                try {
                    Comment($"Package {packageName} not found in R. Now trying to download and install it from cran.rVersions-project.org");
                    var result = EvaluateString($"install.packages('{packageName}', dependencies=TRUE, repos='http://cloud.r-project.org/')");
                } catch (Exception ex) {
                    var message = $"R package {packageName} was not installed and could not be downloaded and installed from the cran website. Please install the package manually within R.";
                    Comment(message);
                    throw new RLoadLibraryException(message, ex);
                }
                libLoaded = EvaluateBoolean($"require('{packageName}')");
                if (libLoaded) {
                    Comment($"R package {packageName} is installed and loaded successfully.");
                } else {
                    var error = GetErrorMessage();
                    var message = $"Tried to download and install R package {packageName} but it could NOT be loaded. Please install the R package manually.";
                    Comment(message);
                    if (!string.IsNullOrEmpty(error)) {
                        Comment($"Message: {error}");
                    }
                    throw new RLoadLibraryException(message);
                }
            } else {
                Comment($"R package {packageName} is loaded successfully.");
            }
            var libraryVersion = EvaluateString($"as.character(packageVersion('{packageName}'))");
            var installedPackageVersion = new Version(libraryVersion);
            if (minimalRequiredPackageVersion != null && installedPackageVersion < minimalRequiredPackageVersion) {
                var msg = $"Version of R package {packageName} (version {installedPackageVersion}) is too old. Please install later version (>= {minimalRequiredPackageVersion}).";
                Comment(msg);
                throw new RLoadLibraryException(msg);
            }
            Comment($"Package version of R package {packageName}: {installedPackageVersion}.");
        }

        #endregion

        /// <summary>
        /// Stub: this method should be implemented in derived classes to
        /// log R commands executed by this engine.
        /// </summary>
        /// <param name="command"></param>
        protected virtual void LogCommand(string command) {
            //System.Diagnostics.Trace.WriteLine(command);
        }
    }
}
