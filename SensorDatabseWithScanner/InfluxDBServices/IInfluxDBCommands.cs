namespace SensorDatabseWithScanner.InfluxDBServices
{
    public interface IInfluxDBCommands
    {
        void CreateDB(string nameOfDB);
        void ShowDB();
        void ShowMeasurments(string DatabaseName);
        void ShowSensorInfo(string mac,string DatabaseName);
        void DeleteKeYValueInMeasurement(string database, string tag_value, string measurement, string tag_key);
        void CheckIfExistInDatabase(string mac, string database, string tag_key, string tag_value);
        void DeleteDatabase(string database);
        public void ExportInfluxDBToCsv(string database);

    }
}