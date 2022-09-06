using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDatabseWithScanner.InfluxDBServices
{
    public class InfluxDBCommands : IInfluxDBCommands
    {
        public void CreateDB(string nameOfDB)
        {
            Console.WriteLine(LinuxCommand.InfluxCommand($"CREATE DATABASE {nameOfDB}"));
        }
        public void ShowDB()
        {
            Console.WriteLine(LinuxCommand.InfluxCommand("SHOW databases"));
        }
        public void ShowMeasurments(string DatabaseName)
        {
           // string DatabaseName = NameOfUseDatabase.GetDatabaseName();
            Console.WriteLine(LinuxCommand.InfluxCommand($"SHOW measurements on {DatabaseName}"));
        }
        public void ShowSensorInfo(string mac,string DatabaseName)
        {
            //string DatabaseName = NameOfUseDatabase.GetDatabaseName();
            string command = $"SELECT * FROM {DatabaseName}.autogen." + '"' +mac+'"';
            Console.WriteLine(LinuxCommand.InfluxCommand(command));
        }
        public void DeleteKeYValueInMeasurement(string database,string tag_value,string measurement,string tag_key)
        {
            //"influx -database=\"TryInflux\" -execute \"DELETE FROM /60:77:71:DB:DC:15/ WHERE SerialNumber='B22040287'\""
            string command = $"influx -execute \\\"DELETE FROM /{measurement}/ WHERE {tag_key}='{tag_value}'\\\" -database=\"{database}\"";
            string a =LinuxCommand.InfluxCommandVoid(command,database);
        }
        public void CheckIfExistInDatabase(string mac,string database,string tag_key,string tag_value)
        {
           string command = $"influx -execute \\\"SELECT * FROM /{mac}/ WHERE \"{tag_key}\"='{tag_value}'\\\" -database=\"{database}\"";
           string a = LinuxCommand.InfluxCommandVoid(command, database);
        }
        public void DeleteDatabase(string database)
        {
            string command = $"DROP DATABASE {database}";
            string a = LinuxCommand.InfluxCommand(command);
        }
        public void ExportInfluxDBToCsv(string database)
        {
            string command = $"influx -database=\"{database}\" -execute 'SELECT * FROM /.*/' -format csv > TemporaryInfluxDB.csv";
            string a = LinuxCommand.SystemCommand(command);
        }
    }
}
