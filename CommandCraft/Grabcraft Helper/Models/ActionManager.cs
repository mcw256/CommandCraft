using Grabcraft_Helper.Model.FileOperations.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Model
{
    /// <summary>
    /// Main class to coordinate everything. I don't know how will I glue it with ViewModel and Program.cs, but I will figure out.
    /// </summary>
    static class ActionManager
    {
        static DataContainer data;
        

        
        
        /// <summary>
        /// Loads neccessary data
        /// </summary>
        public static void Load()
        {
            

            var loadBlockNamesDictionary = new LoadBlockNamesDictionary();
            var response = loadBlockNamesDictionary.Load(); //get resposnse

            data.BlockNamesDictionary = loadBlockNamesDictionary.Output;

        }

        public static void ResetData()
        {
            //todo
        }



    }

    class DataContainer
    {
        public Dictionary<string, string> BlockNamesDictionary { get; set; }
        public Dictionary<string, string> BlockAttributesDictionary { get; set; }



    }

}
