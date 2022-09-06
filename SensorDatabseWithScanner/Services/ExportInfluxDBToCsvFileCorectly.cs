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
        public void Export(string database)
        {
            InfluxDBCommands InfluxCommand = new InfluxDBCommands();
            InfluxCommand.ExportInfluxDBToCsv(database);
            SensorDatabaseToInfluxDB Base = new SensorDatabaseToInfluxDB();
            Base.ReadFromFile("TemporaryInfluxDB.csv");
            //foreach (var val in Base.SensorList)
            //{
            //    string output = "";
            //    foreach (var value in val.Informations)
            //        output += value+";";
            //    Console.WriteLine(val.MAC + " " + val.MainInfo + " " + output);
            //}
            AddSensorsListToCsv.ListOfDeviceModelToCsvFile(Base.SensorList);

            string a=LinuxCommand.SystemCommand("rm TemporaryInfluxDB.csv");
        }
    }
}
