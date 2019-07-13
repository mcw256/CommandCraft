using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LayermapParser.DataTypes;
using Newtonsoft.Json;

namespace LayermapParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //string layermap = LoadLayerMap();

            //List<BuildingsRawNamed> data = LayerMapJsonParser.Parse(layermap);

            //Display(data);



        }

        static string LoadLayerMap()
        {
            string path = @"C:\Users\rivae\Desktop\1 - Troll Hut.json";
            string rawJson = File.ReadAllText(path);
            BuildingFileJsonResponse obj1 = JsonConvert.DeserializeObject<BuildingFileJsonResponse>(rawJson);
            return obj1.LayerMap;
        }

        static void Display(List<BlockRawNamed> data)
        {
            foreach (var item in data)
            {
                Console.WriteLine(item.X);
                Console.WriteLine(item.Y);
                Console.WriteLine(item.Z);
                Console.WriteLine(item.Name);
                Console.WriteLine("---------------");

            }

        }
    }
}
