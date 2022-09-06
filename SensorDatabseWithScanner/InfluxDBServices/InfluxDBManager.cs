using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDatabseWithScanner.InfluxDBServices
{
    public class InfluxDBManager : IInfluxDBManager
    {
        public InfluxDBManager(string name, IInfluxDBInsert dbinsert, IInfluxDBCommands dbcommands)
        {
            DbName = name;
            DBInsert = dbinsert;
            DBcommands = dbcommands;
        }
        public void SetNewDbName(string name)
        {
            DbName = name;
        }

        public string DbName;
        public IInfluxDBInsert DBInsert;
        public IInfluxDBCommands DBcommands;
    }
}
