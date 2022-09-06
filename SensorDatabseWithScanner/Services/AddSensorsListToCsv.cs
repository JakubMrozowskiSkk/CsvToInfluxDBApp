using Common.Modeles;
using CsvHelper;
using SensorDatabseWithScanner.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDatabseWithScanner.Services
{
    public class AddSensorsListToCsv
    {
        private class PartialDiviceModel
        {
            public string? Mac { get; set; }
            public string? SerialNumber { get; set; }
            public string? Info { get; set; }
            
            
        }
        public static void ListOfDeviceModelToCsvFile(IEnumerable<SensorInformationsModel> list)
        {
            var ListPB = new List<PartialDiviceModel>();
            List<SensorInformationsModel> TmpList = list.ToList();
            ListPB = CreateAListToSave(TmpList);
            //Uncoment line below to see avrege package per
            //Console.WriteLine("Avrega PP: " + Avrege_PP.ToString("0.##") + "%");
            using (var writer = new StreamWriter("ExportedDatabase.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(ListPB);
            }
        }
        private static PartialDiviceModel CreataPartialDevice(string mac,string info,IEnumerable<string> informations)
        {
            PartialDiviceModel partialDeviceModel = new PartialDiviceModel();
            if (info == "1")
            {
               partialDeviceModel.Mac = mac;
                string output = "";
                foreach(var val in informations)
                {
                    if (val != "")
                        output += val + "|";
                }
                output = output.Substring(0, output.Length - 1);
               partialDeviceModel.Info = output;
                partialDeviceModel.SerialNumber = "";
            }
            else
            {

                partialDeviceModel.Mac = mac;
                string output = "";
                foreach (var val in informations)
                {
                    if (val != "")
                        output += val;
                }
                partialDeviceModel.Info = "";
                partialDeviceModel.SerialNumber = output;

            }
            return partialDeviceModel;
        }
        private static List<PartialDiviceModel> CreateAListToSave(List<SensorInformationsModel> TemporaryListOfDeviceModel)
        {
            List<PartialDiviceModel> ListOfPartialDeviceModel = new List<PartialDiviceModel>();
            
            foreach (var val in TemporaryListOfDeviceModel)
            {
                int index = ListOfPartialDeviceModel.FindIndex(x => x.Mac.Equals(val.MAC, StringComparison.OrdinalIgnoreCase));
                if(index==-1)
                ListOfPartialDeviceModel.Add(CreataPartialDevice(val.MAC,val.MainInfo,val.Informations));
                else
                {
                    string serialNumber = ListOfPartialDeviceModel[index].SerialNumber;
                    string Informations = ListOfPartialDeviceModel[index].Info;

                    if(val.MainInfo=="1")
                    {
                        string output = "";
                        foreach (var value in val.Informations)
                        {
                            if (value != "")
                                output += value + "|";
                        }
                        output = output.Substring(0, output.Length - 1);
                        Informations += ";" + output;
                        ListOfPartialDeviceModel[index].Info = Informations;
                    }
                    else
                    {
                        string output="";
                        foreach (var value in val.Informations)
                        {
                            if (value != "")
                                output += value;
                        }
                        serialNumber += "|" + output;
                        //serialNumber = serialNumber.Substring(0, serialNumber.Length);
                        ListOfPartialDeviceModel[index].SerialNumber = serialNumber;
                    }
                }
            }
            for (int i = 0; i < ListOfPartialDeviceModel.Count; i++)
                ListOfPartialDeviceModel[i].SerialNumber = ListOfPartialDeviceModel[i].SerialNumber.Substring(1, ListOfPartialDeviceModel[i].SerialNumber.Length - 1);
            return ListOfPartialDeviceModel;
        }
       
    }
}
