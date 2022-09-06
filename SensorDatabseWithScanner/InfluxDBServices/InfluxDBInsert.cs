using Common;
using Common.Modeles;
using SensorDatabseWithScanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDatabseWithScanner.InfluxDBServices
{
    public class InfluxDBInsert : IInfluxDBInsert
    {
        public void InsertSensorToInfluxDB(List<SensorModel> list,string DBName)
        {
           // int i = 1;
            foreach (var val in list)
            {
                //"INSERT INTO TryInflux.autogen A0:00:AD:FO:OD,SerialNumber = SLADCC value = 0";
                string command = $"influx -execute \\\"SELECT * FROM /{val.Mac}/ WHERE \"SerialNumber\"='{val.SerialNumber}'\\\" -database=\"{DBName}\"";
                string cont=LinuxCommand.InfluxCommandVoid(command, DBName);
                if(cont=="")
                {
                    string a = LinuxCommand.InfluxCommand($"INSERT INTO {DBName}.autogen {val.Mac},SerialNumber={val.SerialNumber} value=0");
                }
                //i++;
            }
        }
        public void InsertSensorInfoToInfluxDB(List<SensorInformationsModel> list, string DBName)
        {
            //int i = 1;
            foreach (var val in list)
            {
                string output = val.MainInfo;
                foreach (var tmp in val.Informations)
                    output += ";" + tmp;
                output=output.Replace(" ","_");
                //Console.WriteLine(val.MAC);
                //Console.WriteLine(output);
                //Console.WriteLine(val.SerialNumber);
                string command = $"influx -execute \\\"SELECT * FROM /{val.MAC}/ WHERE \"Info\"='{output}'\\\" -database=\"{DBName}\"";
                string cont = LinuxCommand.InfluxCommandVoid(command, DBName);
                if (cont == "")
                {
                    string a = LinuxCommand.InfluxCommand($"INSERT INTO {DBName}.autogen {val.MAC},Info={output} value=1");
                }
                string command1 = $"influx -execute \\\"SELECT * FROM /{val.MAC}/ WHERE \"SerialNumber\"='{val.SerialNumber}'\\\" -database=\"{DBName}\"";
                string cont1 = LinuxCommand.InfluxCommandVoid(command1, DBName);
                if (cont1 == "")
                {
                    string b = LinuxCommand.InfluxCommand($"INSERT INTO {DBName}.autogen {val.MAC},SerialNumber={val.SerialNumber} value=0");
                }
                //Console.WriteLine(i);
                //i++;
            }
        }
    }
}
