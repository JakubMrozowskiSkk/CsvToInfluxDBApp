using SensorDatabseWithScanner.Models;
using SensorDatabseWithScanner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDatabseWithScanner.Database
{
    public class SensorDatabaseToInfluxDB
    {
        public IEnumerable<SensorInformationsModel> SensorList { get; private set; }
        public void ReadFromFile(string filename)
        {
            SensorList = new List<SensorInformationsModel>();
            SensorList = CsvToSensorList.ReadSensorsFromCsv(filename);
        }
    }
}
