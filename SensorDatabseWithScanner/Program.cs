using Common;
using SensorDatabseWithScanner;
using SensorDatabseWithScanner.Database;
using SensorDatabseWithScanner.InfluxDBServices;
using Newtonsoft.Json;
using SensorDatabseWithScanner.Services;
using Terminal.Gui;
using NStack;
string DatabaseName = File.ReadAllText(@"DatabaseName.json");
DatabaseOfSensorsMacAndSerialNumber databaseName = JsonConvert.DeserializeObject<DatabaseOfSensorsMacAndSerialNumber>(DatabaseName);
SensorDatabaseBasicInfo Base = new SensorDatabaseBasicInfo();
DirectoryInfo bazy = new DirectoryInfo("BAZY");
FileInfo[] CsvFile = bazy.GetFiles("*.csv");
foreach (var val in CsvFile)
{
    Base.ReadFromFile(val.FullName);
}
InfluxDBCommands UserDB = new InfluxDBCommands();
InfluxDBInsert influxDBInsert = new InfluxDBInsert();
InfluxDBManager IDManager = new InfluxDBManager(databaseName.DatabaseNameToUseOfStart, influxDBInsert, UserDB);
bool BasicMenu = true;
ShowBasicMenu();
void ShowExtendedMenu()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("X-----------------------------------------------------------------------X");
    Console.ForegroundColor = ConsoleColor.Black;
    Console.BackgroundColor = ConsoleColor.Gray;
    Console.Write($"\tCurrently in {IDManager.DbName} Database!");
    Console.BackgroundColor = ConsoleColor.Black;
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\t\t ExtendedMenu:");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("\t 1             - Create new Database");
    Console.WriteLine("\t 2             - Show Databases");
    Console.WriteLine("\t 3             - Set Database to use");
    Console.WriteLine("\t 4             - Show Current Database you are in ");
    Console.WriteLine("\t 5             - Show Database Sensors");
    Console.WriteLine("\t 6             - Find Sensor in Database");
    Console.WriteLine("\t 7             - Import Basic info of sensors to Database");
    Console.WriteLine("\t 8             - Import Extended info of sensors to Database");
    Console.WriteLine("\t 9             - Delete Chosen Record In One Place");
    Console.WriteLine("\t 10            - Delete Chosen Record In whole Database");
    Console.WriteLine("\t 11            - Back To Menu");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("X-----------------------------------------------------------------------X");
}
void ShowBasicMenu()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("X-----------------------------------------------------------------------X");
    Console.ForegroundColor = ConsoleColor.Black;
    Console.BackgroundColor = ConsoleColor.Gray;
    Console.Write($"\tCurrently in {IDManager.DbName} Database!");
    Console.BackgroundColor = ConsoleColor.Black;
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\t\t Menu:");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("\t 1     - Find Sensor in Database");
    Console.WriteLine("\t 2     - Refreah Database");
    Console.WriteLine("\t 3     - Export Searched Sensors to Database");
    Console.WriteLine("\t 4     - Export All Sensors to Database");
    Console.WriteLine("\t 5     - Show Extended Menu");
    Console.WriteLine("\t 6     - Exit Program");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("X-----------------------------------------------------------------------X");
}
//UserDB.ShowDB();
//UserDB.ShowMeasurments();
//UserDB.ShowSensorInfo(Console.ReadLine());


string d = LinuxCommand.SystemCommand("rm SearchExportedDatabase.csv");
while (BasicMenu)
{
    Console.ForegroundColor = ConsoleColor.Red;
    string key = Console.ReadLine();
    Console.ForegroundColor = ConsoleColor.White;
    switch (key)
    {
        case "1":
            //string a = LinuxCommand.SystemCommand("rm TemporaryInfluxDB1.csv");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("If you want to going back to the menu and save your's search: write Exit, next write 3");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Magenta;
                string SerialNumberOrMac = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (SerialNumberOrMac == "Exit")
                {
                    Console.WriteLine("End Of Process");
                    ShowBasicMenu();
                    break;
                }

                Console.WriteLine("Find sensor in Database...");

                StringIsMac check = new StringIsMac();
                string Mac = "";
                if (check.IsMacBool(SerialNumberOrMac))
                {
                    Mac = SerialNumberOrMac;
                }
                else
                {
                    DatabaseSensorFinder finder = new DatabaseSensorFinder();
                    Mac = finder.FindThisSensorMac(SerialNumberOrMac, Base.SensorList);
                }
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(Mac);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                IDManager.DBcommands.ShowSensorInfo(Mac, IDManager.DbName);
                Console.ForegroundColor = ConsoleColor.White;
                IDManager.DBcommands.ExportSensorInfluxDBToCsv(IDManager.DbName,Mac);
            }
            break;
        case "2":
            
            Console.WriteLine($"We have {CsvFile.Length} CsvFiles to load...");
            Console.WriteLine("Reloading Database Will Delete old Database and Create new one of the same name do you want to proceed?(y/n)");
            string y_n = Console.ReadLine();
            if(y_n == "y")
            {
                IDManager.DBcommands.DeleteDatabase(IDManager.DbName);
                IDManager.DBcommands.CreateDB(IDManager.DbName);
                int i = 1;
                foreach (var file in CsvFile)
                {
                    //Console.WriteLine(file.FullName);
                    string ExtendedFileName = file.FullName;
                    SensorDatabaseExtendedInfo extendedInfoOfSensorDatabase = new SensorDatabaseExtendedInfo();
                    extendedInfoOfSensorDatabase.ReadFromFile(ExtendedFileName,file.Name);
                    IDManager.DBInsert.InsertSensorInfoToInfluxDB(extendedInfoOfSensorDatabase.InformationsOfSensorList.ToList(), IDManager.DbName);
                    Console.WriteLine($"End Of Import of {i} file");
                    i++;
                }
                Console.WriteLine("Refreash Database Complete");
                ShowBasicMenu();
            }
            break;
        case "3":
            ExportInfluxDBToCsvFileCorectly export1 = new ExportInfluxDBToCsvFileCorectly();
            export1.ExportFindToDatabase();
            Console.WriteLine("End of Export");
            break;
        case "4":
            ExportInfluxDBToCsvFileCorectly export = new ExportInfluxDBToCsvFileCorectly();
            export.ExportWholeDatabase(IDManager.DbName);
            Console.WriteLine("End of Export");
            break;
        case "5":
            Console.WriteLine("Going to ExtendedMenu...");
            ShowExtendedMenu();
            bool ExtendeMenu = true;
            while (ExtendeMenu)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                string contorl = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                switch (contorl)
                {
                    case "2"://work
                        Console.WriteLine("Showing Databases...");
                        IDManager.DBcommands.ShowDB();
                        Console.WriteLine("End Of Process");
                        ShowExtendedMenu();
                        break;
                    case "4"://work
                        Console.WriteLine("Showing Database you are in now...");
                        Console.WriteLine(IDManager.DbName);
                        Console.WriteLine("End Of Process");
                        ShowExtendedMenu();
                        break;
                    case "11"://work
                        Console.WriteLine("Going back to Menu...");
                        ShowBasicMenu();
                        ExtendeMenu = false;
                        break;
                    case "3"://work
                        Console.WriteLine("Set Database to use...");
                        string database = Console.ReadLine();
                        IDManager.SetNewDbName(database);
                        Console.WriteLine($"Database set to \"{database}\" End Of Process");
                        ShowExtendedMenu();
                        break;
                    case "1":
                        Console.WriteLine("Create Database...");
                        string cdatabase = Console.ReadLine();
                        UserDB.CreateDB(cdatabase);
                        Console.WriteLine($"You Create Database name \"{cdatabase}\" End Of Process");
                        ShowExtendedMenu();
                        break;
                    case "5"://work
                        Console.WriteLine("Showing Database Measurments...");
                        IDManager.DBcommands.ShowMeasurments(IDManager.DbName);
                        Console.WriteLine("End Of Process");
                        ShowExtendedMenu();
                        break;
                    case "7"://work
                        Console.WriteLine("Import your .csv file with basic info of sensor to Database...");
                        string BasicFileName = Console.ReadLine();
                        SensorDatabaseBasicInfo basicInfoOfSensorDatabase = new SensorDatabaseBasicInfo();
                        basicInfoOfSensorDatabase.ReadFromFile(BasicFileName);
                        IDManager.DBInsert.InsertSensorToInfluxDB(basicInfoOfSensorDatabase.SensorList, IDManager.DbName);
                        Console.WriteLine("End Of Import");
                        ShowExtendedMenu();
                        break;
                    case "8"://work
                        Console.WriteLine("Import your .csv file with extended info of sensor to Database...");
                        string ExtendedFileName = Console.ReadLine();
                        SensorDatabaseExtendedInfo extendedInfoOfSensorDatabase = new SensorDatabaseExtendedInfo();
                        extendedInfoOfSensorDatabase.ReadFromFile(ExtendedFileName,ExtendedFileName);
                        IDManager.DBInsert.InsertSensorInfoToInfluxDB(extendedInfoOfSensorDatabase.InformationsOfSensorList.ToList(), IDManager.DbName);
                        Console.WriteLine("End Of Import");
                        ShowExtendedMenu();
                        break;
                    case "6"://work
                        string b = LinuxCommand.SystemCommand("rm TemporaryInfluxDB1.csv");
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("If you want to going back to the menu and save your's search: write Exit, next write 3");
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            string SerialNumberOrMac = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            if (SerialNumberOrMac == "Exit")
                            {
                                Console.WriteLine("End Of Process");
                                ShowExtendedMenu();
                                break;
                            }

                            Console.WriteLine("Find sensor in Database...");

                            StringIsMac check = new StringIsMac();
                            string Mac = "";
                            if (check.IsMacBool(SerialNumberOrMac))
                            {
                                Mac = SerialNumberOrMac;
                            }
                            else
                            {
                                DatabaseSensorFinder finder = new DatabaseSensorFinder();
                                Mac = finder.FindThisSensorMac(SerialNumberOrMac, Base.SensorList);
                            }
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine(Mac);
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            IDManager.DBcommands.ShowSensorInfo(Mac, IDManager.DbName);
                            Console.ForegroundColor = ConsoleColor.White;
                            IDManager.DBcommands.ExportSensorInfluxDBToCsv(IDManager.DbName, Mac);
                        }
                        break;
                    case "9":
                        Console.WriteLine("Write Mac: ");
                        string M = "";
                        M = Console.ReadLine();
                        string tag_key = "";
                        string tag_value = "";
                        Console.WriteLine("Input tag_key: ");
                        tag_key = Console.ReadLine();
                        Console.WriteLine("Input tag_value: ");
                        tag_value = Console.ReadLine();
                        //foreach (var val in Base.SensorList)
                        IDManager.DBcommands.DeleteKeYValueInMeasurement(IDManager.DbName, tag_value, M, tag_key);
                        Console.WriteLine("End Of Process");
                        break;
                    case "10":
                        string tag_key_all = "";
                        string tag_value_all = "";
                        Console.WriteLine("Input tag_key: ");
                        tag_key_all = Console.ReadLine();
                        Console.WriteLine("Input tag_value: ");
                        tag_value_all = Console.ReadLine();
                        foreach (var val in Base.SensorList)
                        {
                            if (val.Mac != "")
                                IDManager.DBcommands.DeleteKeYValueInMeasurement(IDManager.DbName, tag_value_all, val.Mac, tag_key_all);
                        }
                        Console.WriteLine("End Of Process");
                        break;
                    default:
                        ShowExtendedMenu();
                        break;
                }
            }
            break;
        case "6":
            Console.WriteLine("Exiting Program...");
            BasicMenu = false;
            string c = LinuxCommand.SystemCommand("rm TemporaryInfluxDB1.csv");
            break;
        default:
            ShowBasicMenu();
            break;
    }
        
}
