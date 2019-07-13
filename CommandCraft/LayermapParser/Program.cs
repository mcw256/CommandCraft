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
            Rootobject obj1 = JsonConvert.DeserializeObject<Rootobject>(rawJson);
            return obj1.LayerMap;
        }

        static void Display(List<BuildingsRawNamed> data)
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

        public class Rootobject
        {
            public string Name { get; set; }
            public string URL { get; set; }
            public string Category { get; set; }
            public string LayerMap { get; set; }
        }




    }
}
