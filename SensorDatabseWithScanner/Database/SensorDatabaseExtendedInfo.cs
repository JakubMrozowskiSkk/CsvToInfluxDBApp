using Common;
using SensorDatabseWithScanner.Models;
using SensorDatabseWithScanner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDatabseWithScanner.Database
{
    public class SensorDatabaseExtendedInfo
    {
        public IEnumerable<SensorInformationsModel> InformationsOfSensorList { get; private set; }
        public void ReadFromFile(string filename,string FullPath)
        {
            InformationsOfSensorList = new List<SensorInformationsModel>();
            InformationsOfSensorList = CsvToExtendedSensorList.ReadExtendedSensorsInfoFromCsv(filename,FullPath);
        }
    }
}
