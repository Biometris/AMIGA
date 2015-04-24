using System.Collections.Generic;
namespace Biometris.DataFileReader {
    public interface IDataFileReader {
        List<T> ReadDataSet<T>(string filename, TableDefinition tableDefinition)
            where T : new();
    }
}
