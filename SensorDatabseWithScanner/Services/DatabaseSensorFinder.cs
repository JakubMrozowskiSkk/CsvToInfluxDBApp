using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Modeles;
namespace SensorDatabseWithScanner.Services
{
    public class DatabaseSensorFinder
    {
        public string FindThisSensorMac(string SerialNumber, List<SensorModel> list)
        {
            int index = list.FindIndex(x => x.SerialNumber == SerialNumber);
            if (index == -1)
                return "";
            else
                return list[index].Mac;
        }
    }
}
