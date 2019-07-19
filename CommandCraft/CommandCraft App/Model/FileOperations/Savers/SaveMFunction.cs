using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandCraft_App.Model.DataTypes;

namespace CommandCraft_App.Model.FileOperations.Savers
{
    class SaveMFunction : Saver<MFunction>
    {
        public override Response Save(MFunction mFunction, string path)
        {
            try
            {
                File.WriteAllText($@"{path}\{mFunction.Name}.mcfunction", mFunction.ToString());
            }
            catch (DirectoryNotFoundException)
            {
                return new Response(true, "Invalid directory error");            
            }
            catch (UnauthorizedAccessException)
            {
                return new Response(true, "No permission to write in this directory error");
            }
            catch (System.Security.SecurityException)
            {
                return new Response(true, "No permission to write in this directory error");
            }
            catch (Exception)
            {
                return new Response(true, "Error");
            }

            return new Response(false, "");
        }
    }
}
