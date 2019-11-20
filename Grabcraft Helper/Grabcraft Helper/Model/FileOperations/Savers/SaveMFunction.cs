using Grabcraft_Helper.DataTypes;
using Grabcraft_Helper.Model.DataTypes;
using Grabcraft_Helper.Model.FileOperations.Savers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.Models.FileOperations.Savers
{
    class SaveMFunction : Saver<MFunction>
    {
        public override Response Save(MFunction mFunction)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.FileName = $"{mFunction.Name}"; 
                saveDialog.Filter = "Minecraft Function File(.mfunction)|*.mfunction|Text Document(.txt)|*.txt"; 

                Nullable<bool> result = saveDialog.ShowDialog();

                if (result == true)
                {
                    if (saveDialog.FilterIndex == 1)
                        File.WriteAllText($@"{saveDialog.FileName}", mFunction.ToString()); //TODO, this needs to be tested


                    if (saveDialog.FilterIndex == 2)
                        File.WriteAllText($@"{saveDialog.FileName}", mFunction.ToString());

                }
                else return new Response(false, "user canceled");

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
