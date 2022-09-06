using Common;
using Common.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDatabseWithScanner.Services
{
    public class CsvToBasicSensorList
    {
        public static List<SensorModel> ReadBasicSensorsInfoFromCsv(string fullPath)
        {
            var sensorList = new List<SensorModel>();
            using(var reader = new StreamReader(fullPath))
            {
                while(!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var splited = line.Split(";");
                    var SplitedMacCorrection = System.Text.RegularExpressions.Regex.Replace(splited[0], @"\s+", "");
                    StringIsMac check = new StringIsMac();
                    //Console.WriteLine(SplitedMacCorrection);
                    if (SplitedMacCorrection.Equals("mac", StringComparison.OrdinalIgnoreCase) || SplitedMacCorrection.Equals("serialnumber", StringComparison.OrdinalIgnoreCase) || SplitedMacCorrection.Equals("sn", StringComparison.OrdinalIgnoreCase))
                        continue;
                    bool MacCheck = check.IsMacBool(SplitedMacCorrection);
                    SensorModel tmp;
                    if (MacCheck)
                    {
                        tmp = new SensorModel(splited[0], splited[1]);
                    }
                    else
                    {
                        tmp = new SensorModel(splited[1], splited[0]);
                    }
                    sensorList.Add(tmp);
                }
            }
            return sensorList;
        }
    }
}
