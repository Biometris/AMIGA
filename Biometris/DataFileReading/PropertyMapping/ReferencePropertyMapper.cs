using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometris.DataFileReading.PropertyMapping {
    public class ReferenceMapper<T> : IPropertyMapper {

        public ReferenceMapper(string targetPropertyName, Dictionary<string, T> lookupTable) : base(targetPropertyName) {
            TargetPropertyName = targetPropertyName;
            LookupTable = lookupTable;
        }

        public Dictionary<string, T> LookupTable { get; set; }

        public override void mapProperty<Target>(object rawValue, Target targetRecord) {
            var targetRecordType = typeof(Target);
            var value = LookupTable[rawValue.ToString()];
            targetRecordType.GetProperty(TargetPropertyName).SetValue(targetRecord, value, null);
        }
    }
}
