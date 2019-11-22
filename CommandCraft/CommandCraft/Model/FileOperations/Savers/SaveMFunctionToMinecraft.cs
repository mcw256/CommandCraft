using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandCraft.DataTypes;
using CommandCraft.Model.DataTypes;
using CommandCraft.Model.FileOperations.Savers.SaveMFunctionUtils;

namespace CommandCraft.Model.FileOperations.Savers
{
    class SaveMFunctionToMinecraft : Saver<MFunction, string, string>
    {
        public override Response Save(MFunction mFunction, string minecraftPath, string saveName)
        {
            try
            {
                StringBuilder currentPath = new StringBuilder($@"{minecraftPath}\saves\{saveName}");

                if (Directory.Exists(currentPath.ToString()) == false) // check if save folder exist
                    return new Response(true, "save directory does not exist! error"); // if not return error

                if (Directory.Exists($@"{currentPath.ToString()}\datapacks") == false) // check if datapacks folder exist
                    return new Response(true, "datapacks directory does not exist! error"); // if not return error

                /*
                 * Let the Spaghetti ifs begin!
                 * :DDDD
                 * All this code below, basically checks whether minecraft save dir is correct, and fixes it(creates folders or files) if it isnt
                */

                currentPath.Append(@"\datapacks"); 

                if (Directory.Exists($@"{currentPath.ToString()}\commandcraft") == false)
                {
                    Directory.CreateDirectory($@"{currentPath.ToString()}\commandcraft");
                    currentPath.Append(@"\commandcraft");
                    Utils.CreatePackmcmetaFile(currentPath.ToString());
                    Directory.CreateDirectory($@"{currentPath.ToString()}\data");
                    currentPath.Append(@"\data");
                    Directory.CreateDirectory($@"{currentPath.ToString()}\commandcraft");
                    currentPath.Append(@"\commandcraft");
                    Directory.CreateDirectory($@"{currentPath.ToString()}\functions");
                }
                else
                {
                    currentPath.Append(@"\commandcraft");

                    if (File.Exists($@"{currentPath.ToString()}\pack.mcmeta") == false)
                        Utils.CreatePackmcmetaFile(currentPath.ToString());

                    if (Directory.Exists($@"{currentPath.ToString()}\data") == false)
                    {
                        Directory.CreateDirectory($@"{currentPath.ToString()}\data");
                        currentPath.Append(@"\data");
                        Directory.CreateDirectory($@"{currentPath.ToString()}\commandcraft");
                        currentPath.Append(@"\commandcraft");
                        Directory.CreateDirectory($@"{currentPath.ToString()}\functions");
                    }
                    else
                    {
                        currentPath.Append(@"\data");

                        if (Directory.Exists($@"{currentPath.ToString()}\commandcraft") == false)
                        {
                            Directory.CreateDirectory($@"{currentPath.ToString()}\commandcraft");
                            currentPath.Append(@"\commandcraft");
                            Directory.CreateDirectory($@"{currentPath.ToString()}\functions");
                        }
                        else
                        {
                            currentPath.Append(@"\commandcraft");
                            if (Directory.Exists($@"{currentPath.ToString()}\functions") == false)
                                Directory.CreateDirectory($@"{currentPath.ToString()}\functions");
                        }
                    }
                }
                currentPath.Append(@"\functions");
                File.WriteAllText($@"{currentPath.ToString()}\{mFunction.Name}.mcfunction", mFunction.ToString());

                

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
