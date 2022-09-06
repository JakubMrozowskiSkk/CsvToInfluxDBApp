using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDatabseWithScanner.Models
{
    public class SensorInformationsModel
    {
        public SensorInformationsModel(string mac,string sn,string mainInfo,IEnumerable<string> informations)
        {
            MAC = mac;
            SerialNumber = sn;
            MainInfo = mainInfo;
            Informations = informations;
        }
        public string MAC { private set; get; }
        public string SerialNumber { private set; get; }
        public string MainInfo { private set; get; }
        public IEnumerable<string> Informations { private set; get; }
    }
}
