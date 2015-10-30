using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Biometris.ExtensionMethods {
    public static class StringExtensionMethods {

        /// <summary>
        /// Parses a string representing a range of values into a sequence of integers.
        /// </summary>
        /// <param name="s">String to parse</param>
        /// <param name="minValue">Minimum value for open range specifier</param>
        /// <param name="maxValue">Maximum value for open range specifier</param>
        /// <returns>An enumerable sequence of integers</returns>
        /// <remarks>
        /// The range is specified as a string in the following forms or combination thereof:
        /// 5           single value
        /// 1,2,3,4,5   sequence of values
        /// 1-5         closed range
        /// -5          open range (converted to a sequence from minValue to 5)
        /// 1-          open range (converted to a sequence from 1 to maxValue)
        /// 
        /// The value delimiter can be either ',' or ';' and the range separator can be
        /// either '-' or ':'. Whitespace is permitted at any point in the input.
        /// 
        /// Any elements of the sequence that contain non-digit, non-whitespace, or non-separator
        /// characters or that are empty are ignored and not returned in the output sequence.
        /// </remarks>
        public static IEnumerable<int> ParseRange(this string s, int minValue, int maxValue) {
            const string pattern = @"(?:^|(?<=[,;]))                  # match must begin with start of string or delim, where delim is , or ;
                                 \s*(                                 # leading whitespace
                                 (?<from>\d*)\s*(?:-|:)\s*(?<to>\d+)  # capture 'from <sep> to' or '<sep> to', where <sep> is - or :
                                 |  		                          # or
                                 (?<from>\d+)\s*(?:-|:)\s*(?<to>\d*)  # capture 'from <sep> to' or 'from <sep>', where <sep> is - or :
                                 |                                    # or
                                 (?<num>\d+)                          # capture lone number
                                 )\s*                                 # trailing whitespace
                                 (?:(?=[,;\b])|$)                     # match must end with end of string or delim, where delim is , or ;";

            var regx = new Regex(pattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

            foreach (Match m in regx.Matches(s)) {
                Group gpNum = m.Groups["num"];
                if (gpNum.Success) {
                    yield return int.Parse(gpNum.Value);
                } else {
                    Group gpFrom = m.Groups["from"];
                    Group gpTo = m.Groups["to"];
                    if (gpFrom.Success || gpTo.Success) {
                        int from = (gpFrom.Success && gpFrom.Value.Length > 0 ? int.Parse(gpFrom.Value) : minValue);
                        int to = (gpTo.Success && gpTo.Value.Length > 0 ? int.Parse(gpTo.Value) : maxValue);

                        for (int i = from; i <= to; i++) {
                            yield return i;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Can be used to replace characters from a string that are not valid as a filename by any other string, eg "_"
        /// Example: string validfilename = invalidfilename.ReplaceInvalidChars("_");
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="replacement">The replacement.</param>
        /// <returns></returns>
        public static string ReplaceInvalidChars(this string filename, string replacement) {
            if (filename == null || filename.Length == 0) return String.Empty;
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex replaceInvalidChars = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return replaceInvalidChars.Replace(filename, replacement);
        }

        /// <summary>
        /// Tries to convert the string to the desired conversion type.
        /// </summary>
        /// <param name="rawValue"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object ConvertToType(this string rawValue, Type conversionType) {
            if (conversionType == null) {
                throw new ArgumentNullException("Conversion type cannot be null");
            } else if (conversionType == typeof(double) || conversionType == typeof(Double)) {
                double value;
                if (double.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out value)) {
                    return value;
                } else {
                    return double.NaN;
                }
            } else if (conversionType == typeof(int)) {
                int value;
                if (int.TryParse(rawValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out value)) {
                    return value;
                } else {
                    return -1;
                }
            } else if (conversionType == typeof(bool)) {
                if (rawValue.ToLower() == "true" || rawValue == "1") {
                    return true;
                } else {
                    return false;
                }
            } else if (conversionType.BaseType == typeof(Enum)) {
                return Enum.Parse(conversionType, rawValue, true);
            } else if (conversionType == typeof(String)) {
                return rawValue;
            }
            throw new Exception("Unknown conversion type");
        }

        /// <summary>
        /// Gets a byte array from the string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string str) {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// An alternative ToString method for objects that formats the string
        /// culture invariant (applicable, e.g., for doubles).
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToInvariantString(this object obj) {
            return obj is IConvertible ? ((IConvertible)obj).ToString(CultureInfo.InvariantCulture)
                : obj is IFormattable ? ((IFormattable)obj).ToString(null, CultureInfo.InvariantCulture)
                : obj.ToString();
        }

        /// <summary>
        /// Shortens a pathname for display purposes.
        /// </summary>
        /// <param labelName="pathname">The pathname to shorten.</param>
        /// <param labelName="maxLength">The maximum number of characters to be displayed.</param>
        /// <remarks>Shortens a pathname by either removing consecutive components of a path
        /// and/or by removing characters from the end of the filename and replacing
        /// then with three elipses (...)
        /// <para>In all cases, the root of the passed path will be preserved in it's entirety.</para>
        /// <para>If a UNC path is used or the pathname and maxLength are particularly short,
        /// the resulting path may be longer than maxLength.</para>
        /// <para>This method expects fully resolved pathnames to be passed to it.
        /// (Use Path.GetFullPath() to obtain this.)</para>
        /// </remarks>
        /// <returns></returns>
        static public string ShortenPathname(this string pathname, int maxLength) {
            if (pathname.Length <= maxLength) {
                return pathname;
            }

            var root = Path.GetPathRoot(pathname);
            if (root.Length > 3) {
                root += Path.DirectorySeparatorChar;
            }

            var elements = pathname.Substring(root.Length).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            int filenameIndex = elements.GetLength(0) - 1;

            if (elements.GetLength(0) == 1) {
                // Pathname is just a root and filename 
                if (elements[0].Length > 5) {
                    // Long enough to shorten
                    if (root.Length + 6 >= maxLength) {
                        // If path is a UNC path, root may be rather long
                        return root + elements[0].Substring(0, 3) + "...";
                    } else {
                        return pathname.Substring(0, maxLength - 3) + "...";
                    }
                }
            } else if ((root.Length + 4 + elements[filenameIndex].Length) > maxLength) {
                // Pathname is just a root and filename
                root += "...\\";
                var len = elements[filenameIndex].Length;
                if (len < 6) {
                    return root + elements[filenameIndex];
                }

                if ((root.Length + 6) >= maxLength) {
                    len = 3;
                } else {
                    len = maxLength - root.Length - 3;
                }
                return root + elements[filenameIndex].Substring(0, len) + "...";
            } else if (elements.GetLength(0) == 2) {
                return root + "...\\" + elements[1];
            } else {
                var len = 0;
                var begin = 0;
                for (int i = 0; i < filenameIndex; i++) {
                    if (elements[i].Length > len) {
                        begin = i;
                        len = elements[i].Length;
                    }
                }
                var totalLength = pathname.Length - len + 3;
                var end = begin + 1;
                while (totalLength > maxLength) {
                    if (begin > 0) {
                        totalLength -= elements[--begin].Length - 1;
                    }
                    if (totalLength <= maxLength) {
                        break;
                    }
                    if (end < filenameIndex) {
                        totalLength -= elements[++end].Length - 1;
                    }
                    if (begin == 0 && end == filenameIndex) {
                        break;
                    }
                }
                // Assemble final string
                for (int i = 0; i < begin; i++) {
                    root += elements[i] + '\\';
                }
                root += "...\\";
                for (int i = end; i < filenameIndex; i++) {
                    root += elements[i] + '\\';
                }
                return root + elements[filenameIndex];
            }
            return pathname;
        }
    }
}
