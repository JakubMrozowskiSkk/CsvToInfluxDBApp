using Common;
using Common.Modeles;
using SensorDatabseWithScanner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDatabseWithScanner.Database
{
    public class SensorDatabaseBasicInfo
    {
        public List<SensorModel> SensorList { private set; get; } = new List<SensorModel>();
        public void ReadFromFile(string filename)
        {
            SensorList.AddRange(CsvToBasicSensorList.ReadBasicSensorsInfoFromCsv(filename));//this sepccific line open csv file and phrase it in list with have 2 spaces like this <##,##>;
        }
    }
}
