using Common;
using SensorDatabseWithScanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDatabseWithScanner.Services
{
    public class CsvToSensorList
    {
        public static List<SensorInformationsModel> ReadSensorsFromCsv(string fullPath)
        {
            var sensorList = new List<SensorInformationsModel>();
            using (var reader = new StreamReader(fullPath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var splited = line.Split(';',',');
                    var SplitedMacCorrection = System.Text.RegularExpressions.Regex.Replace(splited[0], @"\s+", "");
                    StringIsMac check = new StringIsMac();
                    //Console.WriteLine(SplitedMacCorrection);
                    if (SplitedMacCorrection.Equals("name", StringComparison.OrdinalIgnoreCase) || SplitedMacCorrection.Equals("time", StringComparison.OrdinalIgnoreCase) || SplitedMacCorrection.Equals("sn", StringComparison.OrdinalIgnoreCase))
                        continue;
                    bool MacCheck = check.IsMacBool(SplitedMacCorrection);
                    List<string> TmpList = new List<string>();
                    if (splited.Length > 2)
                        for (int i = 2; i < splited.Length-1; i++)
                        {
                            TmpList.Add(splited[i]);
                        }
                    SensorInformationsModel tmp;
                    int length = splited.Length;
                    if (MacCheck)
                    {
                        if (splited[length- 1] == "1")
                        tmp = new SensorInformationsModel(splited[0], splited[1], "1", TmpList);
                        else
                        tmp = new SensorInformationsModel(splited[0], splited[1], "0", TmpList);
                    }
                    else
                    {
                        if (splited[length - 1] == "1")
                            tmp = new SensorInformationsModel(splited[1], splited[0], "1", TmpList);
                        else
                            tmp = new SensorInformationsModel(splited[1], splited[0], "0", TmpList);
                    }
                    sensorList.Add(tmp);
                }
            }
            return sensorList;
        }
    }
}
