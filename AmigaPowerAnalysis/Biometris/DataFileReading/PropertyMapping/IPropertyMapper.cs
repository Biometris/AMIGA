using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometris.DataFileReading.PropertyMapping {
    public abstract class IPropertyMapper {

        protected IPropertyMapper(string targetPropertyName) {
            TargetPropertyName = targetPropertyName;
        }

        public string TargetPropertyName { get; set; }

        public abstract void mapProperty<T>(object rawValue, T record);
    }
}
