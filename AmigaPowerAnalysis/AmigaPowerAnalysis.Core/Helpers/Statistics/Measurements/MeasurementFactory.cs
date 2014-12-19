using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaPowerAnalysis.Helpers.Statistics.Measurements {
    static class MeasurementFactory {
        public static double Modify(double mean, double modifier, MeasurementType measurementType) {
            var result = 0.0;
            switch (measurementType) {
                case MeasurementType.Count:
                    return mean * modifier;
                case MeasurementType.Fraction:
                    result = 1 / (1 + ((1 - mean) / (modifier * mean)));
                    return result;
                case MeasurementType.Continuous:
                    return mean + modifier;
                default:
                    return mean * modifier;
            }
            //if (measurementType == MeasurementType.Fraction) {
            //    return UtilityFunctions.InvLogit(UtilityFunctions.Logit(mean) + modifier);
            //}
        }
    }
}
