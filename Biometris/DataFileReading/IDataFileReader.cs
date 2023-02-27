using System.Collections.Generic;
namespace Biometris.DataFileReader {
    public interface IDataFileReader {
        List<T> ReadDataSet<T>(TableDefinition tableDefinition)
            where T : new();
    }
}
