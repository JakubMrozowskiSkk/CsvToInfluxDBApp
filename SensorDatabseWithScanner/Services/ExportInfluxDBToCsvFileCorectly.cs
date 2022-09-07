using Common;
using SensorDatabseWithScanner.Database;
using SensorDatabseWithScanner.InfluxDBServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDatabseWithScanner.Services
{
    public class ExportInfluxDBToCsvFileCorectly
    {
        public void ExportAllSensorsToDatabase(string database)
        {
            InfluxDBCommands InfluxCommand = new InfluxDBCommands();
            InfluxCommand.ExportWholeDatabaseInfluxDBToCsv(database);
            SensorDatabaseToInfluxDB Base = new SensorDatabaseToInfluxDB();
            Base.ReadFromFile("TemporaryInfluxDB.csv");
            AddSensorsListToCsv.ListOfDeviceModelToCsvFile(Base.SensorList,"AllExportedDatabase.csv");

            string a=LinuxCommand.SystemCommand("rm TemporaryInfluxDB.csv");
        }
        public void ExportFindSensorsToDatabase()
        {
            SensorDatabaseToInfluxDB Base = new SensorDatabaseToInfluxDB();
            Base.ReadFromFile("TemporaryInfluxDB1.csv");
            AddSensorsListToCsv.ListOfDeviceModelToCsvFile(Base.SensorList, "SearchExportedDatabase.csv");
        }
    }
}
