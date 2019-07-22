using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandCraft_App.Model.DataTypes;

namespace CommandCraft_App.Model.FileOperations.Loaders
{
    class LoadPlayerSavesList : Loader<List<string>, string>
    {
        public override List<string> Output { get; protected set; } = new List<string>();

        public override Response Load(string minecraftPath)
        {
            try
            {
                //C:\Users\josh\AppData\Roaming\.minecraft\saves
                var result = Directory.GetDirectories($@"{minecraftPath}\saves").Select(Path.GetFileName).ToList();

                if (result.Count == 0)
                    return new Response(true, "Can't find any game saves!");

            }
            catch (UnauthorizedAccessException)
            {
                return new Response(true, "Unauthorized access error");
            }
            catch (DirectoryNotFoundException)
            {
                return new Response(true, "Directory not found error");
            }
            catch (Exception)
            {
                return new Response(true, "Error");
            }

            return new Response(false, "");
        }
    }
}
