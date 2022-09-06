using Common.Modeles;
using SensorDatabseWithScanner.Models;

namespace SensorDatabseWithScanner.InfluxDBServices
{
    public interface IInfluxDBInsert
    {
        void InsertSensorInfoToInfluxDB(List<SensorInformationsModel> list, string DBName);
        void InsertSensorToInfluxDB(List<SensorModel> list, string DBName);
    }
}